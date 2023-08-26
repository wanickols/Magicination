using Ink.Runtime;
using UnityEngine;

namespace MGCNTN.Core
{
    [CreateAssetMenu(fileName = "Interactions", menuName = "Dialog Interaction")]
    public class DialogInteraction : Interaction
    {

        public string Name;
        public Story InkJSON;
        public Sprite Sprite;

        public override void StartInteraction()
        {
            if (!Game.manager.stateManager.tryState(GameState.Dialogue))
                return;

            DialogueManager.instance.EnableDialogueMode(InkJSON, Sprite, Name);
            Debug.Log("Interaction Successful");

        }
    }
}
