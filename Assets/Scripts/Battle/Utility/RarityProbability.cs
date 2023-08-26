using System;
using Unity.Collections;
using UnityEngine;

namespace MGCNTN.Core
{
    [Serializable]
    public struct RarityProbability
    {


        [ReadOnly] public EnemyRarity rarity;
        [SerializeField] public int probability;

        public RarityProbability(EnemyRarity rarity, int probability)
        {
            this.rarity = rarity;
            this.probability = probability;
        }
    }
}
