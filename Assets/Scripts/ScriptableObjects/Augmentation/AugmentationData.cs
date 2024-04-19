using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "AugmentationData", menuName = ("AugmentationData"))]
    public class AugmentationData : ScriptableObject
    {

        [SerializeField] private bool hasStats = true;

        [SerializeField] private Stats stats;

        [SerializeField] private bool hasStatus;

        [SerializeField] private List<Status> statusList;

        //List of Status

        // The method that creates an augmentation instance from the data
        public Augmentation CreateAugmentation(MemberBattleInfo target)
        {

            List<AugType> types = new List<AugType>();

            if (hasStats)
                types.Add(AugType.Stats);

            if (hasStatus)
                types.Add(AugType.Status);

            return new Augmentation(target, stats, types, statusList);
        }

        public Augmentation CreateAugmentation(MemberBattleInfo target, int duration, int objectID)
        {

            List<AugType> types = new List<AugType>();

            if (hasStats)
                types.Add(AugType.Stats);

            if (hasStatus)
                types.Add(AugType.Status);

            AugmentStats augmentStats = new AugmentStats(duration, objectID);

            return new Augmentation(target, augmentStats, types, statusList);
        }
    }
}