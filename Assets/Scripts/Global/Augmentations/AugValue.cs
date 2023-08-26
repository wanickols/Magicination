using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class AugValue : MonoBehaviour
    {
        [SerializeField] public AugType type = AugType.HP;
        [SerializeField] public int value = 0;
    }
}