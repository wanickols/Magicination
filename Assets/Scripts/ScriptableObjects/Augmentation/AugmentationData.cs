using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "AugmentationData", menuName = ("AugmentationData"))]
    public class AugmentationData : ScriptableObject
    {

        [SerializeField] private string displayName; // The name of the augmentation
        [SerializeField] private string description; // The description of the augmentation
        [SerializeField] private int cost; // The cost of using the augmentation (in MP or item quantity)
        [SerializeField] private float duration; // The duration of the augmentation (in seconds or turns)
        [SerializeField] private List<AugType> types;
        [SerializeField] private List<int> values;

        // The method that creates an augmentation instance from the data
        public Augmentation CreateAugmentation(Stats target, Stats user, bool costMP = false)
        {
            if (types.Count != values.Count)
            {
                Debug.Log("Types and Values not equal");
                return null;
            }

            Dictionary<AugType, int> augs = new Dictionary<AugType, int>();

            int i = 0;
            foreach (var type in types)
            {
                augs.Add(type, values[i]);
                i++;
            }

            return new Augmentation(displayName, description, costMP, cost, duration, target, user, augs);
        }
    }
}