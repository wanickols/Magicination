using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class Augmentation
    {
        public float duration;
        public Stats target;
        public Stats user;
        public Stats augments;
        public List<AugType> types;
        //List of Status

        public Augmentation(float duration, Stats target, Stats user, Stats augments, List<AugType> types)
        {
            this.duration = duration;
            this.target = target;
            this.user = user;
            this.types = types;
        }

        public void ApplyEffect()
        {

            foreach (AugType type in types)
                augment(type);


            //For each status add status

            if (duration > 0) ///FIX ME to work with turns instead of time
                Core.Game.manager.StartCoroutine(CO_RemoveAfterDuration());
        }

        private void augment(AugType type)
        {
            switch (type)
            {
                case AugType.Stats:
                    addStats();
                    break;
                case AugType.Status:
                    augmentStatus();
                    break;
                default:
                    break;
            };
        }

        public void RemoveEffect()
        {

            foreach (AugType type in types)
            {
                switch (type)
                {
                    case AugType.Stats:
                        removeStats();
                        break;
                    case AugType.Status:
                        removeStatus();
                        break;
                    default:
                        break;
                };
            }

        }


        private IEnumerator CO_RemoveAfterDuration()
        {
            yield return new WaitForSeconds(duration);

            RemoveEffect();
        }

        private void addStats() => target += augments;
        private void removeStats() => target -= augments;

        private void augmentStatus()
        {
        }

        private void removeStatus()
        {
        }
    }


}
