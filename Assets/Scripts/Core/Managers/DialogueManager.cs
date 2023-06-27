using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject DialoguePanel;
    private Animator dialogueAnimator;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image Headshot;



    [Header("Globals Ink FIle")]
    [SerializeField] private String globalsInkFile;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }


    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    [Header("Choices")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private DialogueVariables dialogueVariables;


    private void Awake()
    {
        instance = this;

        dialogueVariables = new DialogueVariables(globalsInkFile);
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        DialoguePanel.SetActive(false);
        dialogueAnimator = DialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int i = 0;

        foreach (var choice in choices)
        {
            choicesText[i++] = choice.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
            return;

        if (Game.Player.InputHandler.ContinueDialogueCheck())
            ContinueStory();
    }

    public void EnableDialogueMode(Story story, Sprite sprite, string name)
    {

        currentStory = story;
        dialogueVariables.StartListening(story);
        nameText.text = name;
        Headshot.sprite = sprite;

        dialogueIsPlaying = true;
        DialoguePanel.SetActive(true);

        Game.OpenDialogue();
        ContinueStory();
    }


    private void ContinueStory()
    {

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            StartCoroutine(CO_ExitDialogueMode());
        }
    }

    private IEnumerator CO_ExitDialogueMode()
    {

        dialogueAnimator.SetBool("closeDialogue", true);
        yield return new WaitForSeconds(.5f);
        dialogueIsPlaying = false;
        DialoguePanel.SetActive(false);
        dialogueText.text = "";
        Game.CloseDialogue();

    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

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

    public void MakeCHoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;

        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning($"Ink variable was found to be null: {variableName}");
        }
        return variableValue;
    }

}
