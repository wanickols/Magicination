using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public class DialogueManager : MonoBehaviour
    {
        //Events
        public event Action<string, List<Choice>> setDialogueText;
        public event Action<string, Sprite> openDialogue;
        public event Action closeDialogue;


        [Header("Globals Ink FIle")]
        [SerializeField] private String globalsInkFile;

        private Story currentStory;

        public static DialogueManager instance;

        private DialogueVariables dialogueVariables;
        private InputHandler inputHandler;

        public void Init(InputHandler handler)
        {
            this.inputHandler = handler;
            handler.Continue += ContinueStory;
        }


        private void Awake()
        {
            //Singleton implementations (yes I know issues with these)
            if (instance != null && instance != this)
                Destroy(this);
            else
                instance = this;


            dialogueVariables = new DialogueVariables(globalsInkFile);
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        public void EnableDialogueMode(Story story, Sprite sprite, string name)
        {

            openDialogue?.Invoke(name, sprite);

            currentStory = story;
            dialogueVariables.StartListening(story);


            ContinueStory();
        }


        private void ContinueStory()
        {

            if (currentStory.canContinue)
            {
                setDialogueText?.Invoke(currentStory.Continue(), currentStory.currentChoices);
            }
            else
            {
                closeDialogue?.Invoke();
            }
        }
        public void MakeChoice(int choiceIndex)
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

        ~DialogueManager()
        {
            inputHandler.Continue -= ContinueStory;
        }

    }
}
