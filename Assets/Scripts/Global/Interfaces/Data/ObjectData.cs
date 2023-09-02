using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class ObjectData
    {
        [Header("Basics")]
        public int id;
        public string displayName;
        public string description;
        public int requiredLevel;
        public Sprite sprite;

        [Header("Augmentation")]
        public AugmentationData augData;
    }
}