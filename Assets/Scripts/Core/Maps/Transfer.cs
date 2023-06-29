using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Transfer : MonoBehaviour
    {

        [SerializeField] private int id;
        [SerializeField] private Map newMap;
        [SerializeField] private int destinationId;

        public int Id => id;
        public Map NewMap => newMap;
        public int DestinationId => destinationId;
    }
}