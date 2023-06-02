using UnityEngine;

public class StationaryNPC : Character, IInteractable
{

    [SerializeField] private Interaction interaction;
    public Interaction Interaction => interaction;

    public void Interact()
    {
        Interaction.StartInteraction();
    }
}
