using UnityEngine;

namespace Core
{
    public class Exit : MonoBehaviour
    {
        private Map currentMap => Game.manager.Map;
        private Vector2Int exitCell;


        [SerializeField] private Map newMap;
        [SerializeField] private Vector2Int destinationCell;

        private void Awake()
        {

        }

        private void Start()
        {
            exitCell = currentMap.grid.GetCell2D(this.gameObject);
            currentMap.Exits.Add(exitCell, this);
        }

        public void TeleportPlayer()
        {
            currentMap.teleportPlayer(newMap, destinationCell);
        }
    }
}