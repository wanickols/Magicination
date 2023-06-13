using Ink.Runtime;
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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        DialoguePanel.SetActive(false);
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
}
