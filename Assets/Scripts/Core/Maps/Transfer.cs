using UnityEngine;

namespace Core
{
    public class Transfer : MonoBehaviour
    {
        private Map currentMap;
        public Vector2Int transferCell { get; private set; }

        [SerializeField] private int id;
        [SerializeField] private Map newMap;
        [SerializeField] private int destinationId;

        public int Id => id;

        private void Awake()
        {

            currentMap = FindAnyObjectByType<Map>();

            transferCell = currentMap.grid.GetCell2D(this.gameObject);
            currentMap.transfers.Add(transferCell, this);
        }

        private void Start()
        {


        }

        public void TeleportPlayer()
        {
            currentMap.teleportPlayer(newMap, destinationId);
        }
    }
}