using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Transfer : MonoBehaviour, ITriggerByTouch
    {

        //Events
        public event Action<Transfer> TeleportPlayer;

        [SerializeField] private int id;
        [SerializeField] private Map newMap;
        [SerializeField] private int destinationId;
        [SerializeField] private Vector2Int offset;

        //Accessors
        public Map NewMap => newMap;
        public int DestinationId => destinationId;
        public Vector2Int Offset => offset;



        //Unity Functions
        private void Start()
        {
            Game.manager.Map.Triggers.Add(Cell, this); //adds to map
            TeleportPlayer += Game.manager.LoadMap; //adds game as listener
        }

        //Interface
        public Vector2Int Cell => Game.manager.Map.grid.GetCell2D(gameObject);

        public void Trigger() => TeleportPlayer?.Invoke(this);



        //Check For Destination
        public bool isDestination(Transfer transfer)
        {
            return transfer.DestinationId == id;
        }

        private void OnDestroy()
        {
            TeleportPlayer -= Game.manager.LoadMap;
        }

    }
}