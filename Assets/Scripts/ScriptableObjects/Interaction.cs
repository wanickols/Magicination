using UnityEngine;

[CreateAssetMenu(fileName = "Interactions", menuName = "New Interaction")]
public class Interaction : ScriptableObject
{
    public virtual void StartInteraction()
    {
        Debug.Log("Interaction Successful");
    }
}
