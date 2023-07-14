using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Map : MonoBehaviour
    {

        [SerializeField]
        public Region region;

        public Dictionary<Vector2Int, MonoBehaviour> occupiedCells { get; private set; }

        public Dictionary<Vector2Int, Transfer> transfers { get; private set; }

        public Grid grid { get; private set; }

        public float cellsize => grid.cellSize.x;

        private void Awake()
        {

            occupiedCells = new Dictionary<Vector2Int, MonoBehaviour>();
            transfers = new Dictionary<Vector2Int, Transfer>();
            grid = GetComponent<Grid>();

            //Create Transfers
            Transfer[] transferArray = FindObjectsOfType<Transfer>();

            foreach (Transfer transfer in transferArray)
            {
                Vector2Int cellLocation = grid.GetCell2D(transfer.gameObject);
                transfers.Add(cellLocation, transfer);
            }

        }


        //Functions

        public IInteractable isInteractable(Vector2Int target)
        {
            if (occupiedCells[target] is IInteractable interactable)
                return interactable;
            else
                return null;
        }

        public bool containsKey(Vector2Int cell)
        {
            return occupiedCells.ContainsKey(cell);
        }

        public Vector2Int GetCell2D(GameObject gameObject)
        {
            return grid.GetCell2D(gameObject);
        }

        public Vector2 GetCellCenter2D(Vector2Int cell)
        {
            return grid.GetCellCenter2D(cell);
        }

        public Vector2 GetCellCenter2D(GameObject gameObject)
        {
            return GetCellCenter2D(grid.GetCell2D(gameObject));
        }

        public void addCell(Vector2Int cell, MonoBehaviour mono)
        {
            occupiedCells.Add(cell, mono);
        }

        public void removeCell(Vector2Int cell)
        {
            occupiedCells.Remove(cell);
        }

        //Test Functions
        public MonoBehaviour getOccupuiedCell(Vector2Int cell)
        {
            return occupiedCells[cell];
        }

        public void destroyTransfers()
        {
            foreach (KeyValuePair<Vector2Int, Transfer> t in transfers)
            {
                if (t.Value != null)
                    Destroy(t.Value.gameObject);
            }
        }

    }
}