using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    [Header("Choices")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        DialoguePanel.SetActive(false);

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

    public void EnableDialogueMode(TextAsset inkJSON)
    {

        currentStory = new Story(inkJSON.text);
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
            ExitDialogueMode();
        }
    }

    private void ExitDialogueMode()
    {
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

}
