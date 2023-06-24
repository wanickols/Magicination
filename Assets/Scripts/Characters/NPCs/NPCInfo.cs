using Ink.Runtime;
using System;
using UnityEngine;


//All Story and Info related to a given NPC
[Serializable]
public class NPCInfo
{
    [SerializeField] private string name;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private Sprite sprite;

    public DialogInteraction Interaction;
    public void createInteraction()
    {
        Interaction = new DialogInteraction();
        Interaction.Name = name;
        Story story = new Story(inkJSON.text);

        Interaction.InkJSON = story;

        Interaction.Sprite = sprite;
        Interaction.StartInteraction();
    }

}
