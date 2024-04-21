using System.Collections.Generic;

namespace MGCNTN
{
    public class Augmentation
    {
        public MemberBattleInfo target;
        public Stats augments;
        public List<AugType> types;
        public List<Status> statusList;

        public Augmentation(MemberBattleInfo target, Stats augments, List<AugType> types, List<Status> statusList)
        {
            this.target = target;
            this.augments = augments;
            this.types = types;
            this.statusList = statusList;
        }

        public void ApplyEffect()
        {

            foreach (AugType type in types)
                augment(type);

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

        private void addStats()
        {
            if (augments is AugmentStats) //temp
                target.augmentingStats.Add((AugmentStats)augments);
            else //permanent
                target.baseStats += augments;

        }
        private void augmentStatus() => target.Statuses.Add(statusList);
    }


}
