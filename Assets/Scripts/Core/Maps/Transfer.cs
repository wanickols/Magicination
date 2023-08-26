using System;
using UnityEngine;

namespace MGCNTN.Core
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

        private MapManager mapManager;

        //Accessors
        public Map NewMap => newMap;
        public int DestinationId => destinationId;
        public Vector2Int Offset => offset;



        //Unity Functions
        private void Start()
        {
            mapManager = Game.manager.mapManager;

            mapManager.map.Triggers.Add(Cell, this); //adds to map
            TeleportPlayer += mapManager.LoadMap; //adds game as listener
        }

        //Interface
        public Vector2Int Cell => mapManager.grid.GetCell2D(gameObject);

        public void Trigger() => TeleportPlayer?.Invoke(this);



        //Check For Destination
        public bool isDestination(Transfer transfer)
        {
            return transfer.DestinationId == id;
        }

        private void OnDestroy()
        {
            TeleportPlayer -= mapManager.LoadMap;
        }

    }
}