using UnityEngine;

namespace MGCNTN
{
    [System.Serializable]
    public class Stats
    {
        /// Protected Parameters

        //Const Max Values
        protected const int MAX_POSSIBLE_LEVEL = 99;
        protected const int MAX_POSSIBLE_EXP = 9999999;

        protected const int MAX_POSSIBLE_HP = 9999; //Health Points
        protected const int MAX_POSSIBLE_ENG = 999; //Energy Points

        protected const int MAX_POSSIBLE_ATK = 999; //Physical Attack
        protected const int MAX_POSSIBLE_DEF = 999; //Physical Defense

        protected const int MAX_POSSIBLE_MATK = 999; //Magical Attack
        protected const int MAX_POSSIBLE_MDEF = 999; //Magical Defense

        protected const int MAX_POSSIBLE_SPD = 99; //Speed 
        protected const int MAX_POSSIBLE_EVS = 99; //Evasion

        protected const int MAX_POSSIBLE_ENC = 100; //Encounter Rate


        //Actual stats
        [SerializeField] protected int lv;
        [SerializeField] protected int exp;
        [SerializeField] protected int nxtExp;
        [SerializeField] protected int damageTaken;
        [SerializeField] protected int maxHp;
        [SerializeField] protected int energyUsed;
        [SerializeField] protected int maxEng;
        [SerializeField] protected int atk;
        [SerializeField] protected int matk;
        [SerializeField] protected int def;
        [SerializeField] protected int mdef;
        [SerializeField] protected int spd;
        [SerializeField] protected int evs;
        [SerializeField] protected int enc;


        /// Public Functions
        public Stats() => lv = exp = damageTaken = maxHp = energyUsed = maxEng = atk = def = matk = mdef = spd = evs = 0;

        public Stats(int lv, int exp, int damageTaken, int maxHp, int energyUsed, int maxEng, int atk, int def, int matk, int mdef, int spd, int evs)
        {
            this.lv = lv;
            this.exp = exp;
            this.damageTaken = damageTaken;
            this.maxHp = maxHp;
            this.maxEng = maxEng;
            this.energyUsed = energyUsed;
            this.atk = atk;
            this.matk = matk;
            this.def = def;
            this.mdef = mdef;
            this.spd = spd;
            this.evs = evs;
        }



        public float turnSpeed => (MAX_POSSIBLE_SPD - SPD) / (float)MAX_POSSIBLE_SPD; //Battle order determinant

        public int LV
        {
            get => lv;
            set
            {
                lv = Mathf.Clamp(value, 0, MAX_POSSIBLE_LEVEL);
            }
        }

        public int EXP
        {
            get => exp;
            set
            {
                exp = Mathf.Clamp(value, 0, MAX_POSSIBLE_EXP);
                tryLevelUp();
            }
        }

        private void tryLevelUp() {
            if (nxtExp == 0) return;

            while (exp >= nxtExp && LV < MAX_POSSIBLE_LEVEL && exp < MAX_POSSIBLE_EXP) {
                LV++;
                nxtExp += (int)(nxtExp * 1.5);
            }
        }

        public int NXTEXP
        {
            get => nxtExp;
            set
            {
                nxtExp = Mathf.Clamp(value, 0, MAX_POSSIBLE_EXP);
            }
        }


        public int HP => Mathf.Max(0, MAXHP - damageTaken);

        public int EnergyUsed {
            get => energyUsed;
            set => energyUsed = Mathf.Clamp(value, 0, MAX_POSSIBLE_ENG);
        }

        public int DamageTaken
        {
            get => damageTaken;
            set => damageTaken = Mathf.Clamp(value, 0, MAX_POSSIBLE_HP);
        }

        public int ENG => maxEng - energyUsed;

        public int MAXHP
        {
            get => maxHp;
            set
            {
                maxHp = Mathf.Clamp(value, 0, MAX_POSSIBLE_HP);
            }
        }

        public int MAXENG
        {
            get => maxEng;
            set
            {
                maxEng = Mathf.Clamp(value, 0, MAX_POSSIBLE_ENG);
            }
        }

        public int ATK
        {
            get => atk;
            set
            {
                atk = Mathf.Clamp(value, 0, MAX_POSSIBLE_ATK);
            }
        }

        public int MATK
        {
            get => matk;
            set
            {
                matk = Mathf.Clamp(value, 0, MAX_POSSIBLE_MATK);
            }
        }

        public int DEF
        {
            get => def;
            set
            {
                def = Mathf.Clamp(value, 0, MAX_POSSIBLE_DEF);
            }
        }

        public int MDEF
        {
            get => mdef;
            set
            {
               mdef = Mathf.Clamp(value, 0, MAX_POSSIBLE_MDEF);
            }
        }
        public int SPD
        {
            get => spd;
            set
            {
                spd = Mathf.Clamp(value, 0, MAX_POSSIBLE_SPD);
            }
        }
        public int EVS
        {
            get => evs;
            set
            {
                evs = Mathf.Clamp(value, 0, MAX_POSSIBLE_EVS);
            }
        }

        public int ENC
        {
            get => enc;
            set
            {
                enc = Mathf.Clamp(value, 0, MAX_POSSIBLE_ENC);
            }
        }


        /// Operators
        public static Stats operator +(Stats a, Stats b)
        {
            //NullCheck
            if (a == null && b == null)
                return null;
            else if (a == null)
                return b;
            else if (b == null)
                return a;

            Stats result = new Stats();

            result.LV = a.lv + b.lv;
            result.NXTEXP = a.nxtExp + b.nxtExp;
            result.EXP = a.exp + b.exp;
            result.MAXHP = a.maxHp + b.maxHp;
            result.DamageTaken = a.damageTaken + b.damageTaken;
            result.MAXENG = a.maxEng + b.maxEng;
            result.EnergyUsed = a.energyUsed + b.energyUsed;
            result.ATK = a.atk + b.atk;
            result.DEF = a.def + b.def;
            result.MATK = a.matk + b.matk;
            result.MDEF = a.mdef + b.mdef;
            result.SPD = a.spd + b.spd;
            result.EVS = a.evs + b.evs;
            result.ENC = a.enc + b.enc;

            result.tryLevelUp();

            // Return the new Stats object
            return result;
        }

        public static Stats operator -(Stats a, Stats b)
        {
            //NullCheck
            if (a == null && b == null)
                return null;
            else if (a == null)
                return b;
            else if (b == null)
                return a;

            Stats result = new Stats();

            result.LV = a.lv - b.lv;
            result.NXTEXP = a.nxtExp - b.nxtExp;
            result.EXP = a.exp - b.exp;
            result.MAXHP = a.maxHp - b.maxHp;
            result.DamageTaken = a.damageTaken - b.damageTaken;
            result.MAXENG = a.maxEng + b.maxEng;
            result.EnergyUsed = a.energyUsed - b.energyUsed;
            result.ATK = a.atk - b.atk;
            result.DEF = a.def - b.def;
            result.MATK = a.matk - b.matk;
            result.MDEF = a.mdef - b.mdef;
            result.SPD = a.spd - b.spd;
            result.EVS = a.evs - b.evs;
            result.ENC = a.enc - b.enc;

            return result;
        }
    }
}