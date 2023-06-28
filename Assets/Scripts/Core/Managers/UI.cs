using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject DialoguePanel;
    private Animator dialogueAnimator;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image Headshot;

    //Choices
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    [SerializeField] private GameObject DialogueManagerPrefab;
    private DialogueManager dialogueManager;


    private void Awake()
    {

        dialogueManager = GetComponentInChildren<DialogueManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Dialogue Events
        dialogueManager.setDialogueText += setDialogueText;
        dialogueManager.openDialogue += openDialogue;
        dialogueManager.closeDialogue += closeDialogue;


        //Dialogue Init
        DialoguePanel.SetActive(false);
        dialogueAnimator = DialoguePanel.GetComponent<Animator>();
        choicesText = new TextMeshProUGUI[choices.Length];
        int i = 0;

        foreach (var choice in choices)
        {
            choicesText[i++] = choice.GetComponentInChildren<TextMeshProUGUI>();
        }

        //Other UI

    }




    //Dialgoue
    private void openDialogue(string name, Sprite sprite)
    {

        DialoguePanel.SetActive(true);
        nameText.text = name;
        Headshot.sprite = sprite;
    }

    private void closeDialogue()
    {
        StartCoroutine(CO_closeDialogue());
    }

    private IEnumerator CO_closeDialogue()
    {
        dialogueAnimator.SetBool("closeDialogue", true);
        yield return new WaitForSeconds(.5f);

        DialoguePanel.SetActive(false);
        dialogueText.text = "";
        Game.manager.returnState();
    }

    private void setDialogueText(string text, List<Choice> choices)
    {
        dialogueText.text = text;
        DisplayChoices(choices);
    }

    private void DisplayChoices(List<Choice> currentChoices)
    {

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError($"More choices were given than the UI can support. number of choices given: {currentChoices.Count}");

        }

        int index = 0;
        //Makes choices visible
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            ++index;
        }

        //Sets all other choices to hidden.
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

    }


    //Deconstructor
    ~UI()
    {
        dialogueManager.setDialogueText -= setDialogueText;
        dialogueManager.openDialogue -= openDialogue;
        dialogueManager.closeDialogue -= closeDialogue;
    }
}
