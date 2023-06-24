using Ink.Runtime;
using UnityEngine;
[CreateAssetMenu(fileName = "Interactions", menuName = "Dialog Interaction")]
public class DialogInteraction : Interaction
{

    public string Name;
    public Story InkJSON;
    public Sprite Sprite;
    public override void StartInteraction()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.EnableDialogueMode(InkJSON, Sprite, Name);

            Debug.Log("Interaction Successful");
        }
    }
}
