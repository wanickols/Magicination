using System;

namespace MGCNTN
{
    [Serializable]
    public class AugmentStats : Stats
    {
        public int parentID = 0;
        public int duration = 0;

        public AugmentStats(int lv, int exp, int hp, int maxHp, int mp, int maxMp, int atk, int def, int spd, int evs, int duration, int parentID) : base(lv, exp, hp, maxHp, mp, maxMp, atk, def, spd, evs)
        {
            this.duration = duration;
            this.parentID = parentID;
        }

        public AugmentStats(int duration, int parentID) : base()
        {
            this.duration = duration;
            this.parentID = parentID;
        }

    }
}