using UnityEngine;

[CreateAssetMenu(fileName = "Interactions", menuName = "New Interaction")]
public class Interaction : ScriptableObject
{


    [SerializeField] private TextAsset inkJSON;
    public virtual void StartInteraction()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.EnableDialogueMode(inkJSON);

            Debug.Log("Interaction Successful");
        }
    }
}
