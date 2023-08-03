using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class StartDialogue : ICutCommand
    {
        [SerializeField] private NPC speaker;

        public bool isFinished { get; private set; }

        public IEnumerator CO_Execute()
        {
            speaker.NPCInfo.createInteraction();

            yield return null;
        }


    }
}