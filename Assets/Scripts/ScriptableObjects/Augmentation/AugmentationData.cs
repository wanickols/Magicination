using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "AugmentationData", menuName = ("AugmentationData"))]
    public class AugmentationData : ScriptableObject
    {
        [SerializeField] private bool status;
        public bool stats = true;

        [SerializeField] private Stats augmentStats;


        //List of Status

        // The method that creates an augmentation instance from the data
        public Augmentation CreateAugmentation(Stats target, Stats user, bool costMP = false)
        {

            List<AugType> types = new List<AugType>();

            if (stats)
                types.Add(AugType.Stats);

            if (status)
                types.Add(AugType.Status);

            return new Augmentation(0, target, user, augmentStats, types);
        }
    }
}