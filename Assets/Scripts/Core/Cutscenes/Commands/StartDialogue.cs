using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class StartDialogue : ICutCommand
    {
        [SerializeField] private NPC speaker;


        public bool isFinished { get; private set; } = false;

        public IEnumerator CO_Execute()
        {
            speaker.Interact();

            DialogueManager.instance.closeDialogue += listenForDialogue;

            while (!isFinished)
            {
                yield return null;
            }
        }

        private void listenForDialogue()
        {
            isFinished = true;
        }
    }
}