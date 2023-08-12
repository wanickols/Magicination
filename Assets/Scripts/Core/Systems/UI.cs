using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
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
        private DialogueManager dialogueManager;

        [Header("Menu UI")]
        [SerializeField] private GameObject MainMenuPrefab;

        public MainMenu mainMenu { get; private set; }
        private StateManager stateManager;

        public void init(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        private void Awake()
        {
            initMenu();
            initDialogue();
        }

        // Start is called before the first frame update
        private void Start()
        {
            initEvents();
        }

        private void initMenu()
        {
            GameObject menu = Instantiate(MainMenuPrefab, this.transform);
            mainMenu = menu.GetComponentInChildren<MainMenu>();
        }

        private void initDialogue()
        {
            dialogueManager = GetComponentInChildren<DialogueManager>();

            //Dialogue Init
            DialoguePanel.SetActive(false);
            dialogueAnimator = DialoguePanel.GetComponent<Animator>();
            choicesText = new TextMeshProUGUI[choices.Length];
            int i = 0;

            foreach (var choice in choices)
            {
                choicesText[i++] = choice.GetComponentInChildren<TextMeshProUGUI>();
            }

        }

        private void initEvents()
        {
            //Dialogue Events
            dialogueManager.setDialogueText += setDialogueText;
            dialogueManager.openDialogue += openDialogue;
            dialogueManager.closeDialogue += closeDialogue;

            //Menu Events
            mainMenu.openMenu += openMenu;
            mainMenu.closeMenu += closeMenu;

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
            stateManager.restorePreviousState();
        }

        private IEnumerator CO_closeDialogue()
        {
            dialogueAnimator.SetBool("closeDialogue", true);
            yield return new WaitForSeconds(.5f);
            DialoguePanel.SetActive(false);
            dialogueText.text = "";

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

        private void openMenu() => stateManager.tryState(GameState.Menu);
        private void closeMenu() => stateManager.restorePreviousState();

        //Deconstructor
        ~UI()
        {
            dialogueManager.setDialogueText -= setDialogueText;
            dialogueManager.openDialogue -= openDialogue;
            dialogueManager.closeDialogue -= closeDialogue;


            //Menu
            mainMenu.openMenu -= openMenu;
            mainMenu.closeMenu -= closeMenu;
        }
    }
}
