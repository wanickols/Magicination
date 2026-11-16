using Codice.Client.BaseCommands;
using MGCNTN.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{

    public class Chest : MonoBehaviour, IInteractable
    {

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite closedSprite;
        [SerializeField] private Sprite openSprite;
        [SerializeField] private List<Item> contents;

        private bool opened;

        private Map map => Game.manager.mapManager.map;

        private void Start()
        {
            map.addCell(map.GetCell2D(gameObject), this);
        }

        public void Interact()
        {
            if (opened)
                return;

            // Grant LOOOT
            Debug.Log("Chest Opened");
            spriteRenderer.sprite = openSprite;
            opened = true;
        }
    }
}