using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class Stats
    {
        /// Protected Parameters

        //Const Max Values
        protected const int MAX_POSSIBLE_LEVEL = 99;
        protected const int MAX_POSSIBLE_EXP = 9999999;

        protected const int MAX_POSSIBLE_HP = 9999; //Health Points
        protected const int MAX_POSSIBLE_MP = 9999; //Magic Points

        protected const int MAX_POSSIBLE_ATK = 999; //Physical Attack
        protected const int MAX_POSSIBLE_MATK = 999; //Magic Attack

        protected const int MAX_POSSIBLE_DEF = 999; //Physical Defense
        protected const int MAX_POSSIBLE_MDEF = 999; //Magic Defense

        protected const int MAX_POSSIBLE_SPD = 99; //Speed 
        protected const int MAX_POSSIBLE_EVS = 99; //Evasion

        protected const int MAX_POSSIBLE_ENC = 100; //Encounter Rate


        //Actual stats
        [SerializeField] protected int lv;
        [SerializeField] protected int exp;
        [SerializeField] protected int nxtExp;
        [SerializeField] protected int hp;
        [SerializeField] protected int maxHp;
        [SerializeField] protected int mp;
        [SerializeField] protected int maxMp;
        [SerializeField] protected int atk;
        [SerializeField] protected int matk;
        [SerializeField] protected int def;
        [SerializeField] protected int mdef;
        [SerializeField] protected int spd;
        [SerializeField] protected int evs;
        [SerializeField] protected int enc;


        /// Public Functions
        public Stats() => lv = exp = hp = maxHp = mp = maxMp = atk = matk = def = mdef = spd = evs = 0;

        public Stats(int lv, int exp, int hp, int maxHp, int mp, int maxMp, int atk, int matk, int def, int mdef, int spd, int evs)
        {
            this.lv = lv;
            this.exp = exp;
            this.hp = hp;
            this.maxHp = maxHp;
            this.maxMp = maxMp;
            this.mp = mp;
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
                lv = Mathf.Clamp(value, 1, MAX_POSSIBLE_LEVEL);
            }
        }

        public int EXP
        {
            get => exp;
            set
            {
                exp = Mathf.Clamp(value, 0, nxtExp);
            }
        }

        public int NXTEXP
        {
            get => (int)(exp * 1.5f); //todo make a better func for this
            set
            {
                nxtExp = Mathf.Clamp(value, 0, MAX_POSSIBLE_EXP);
            }
        }


        public int HP
        {
            get => hp;
            set
            {
                hp = Mathf.Clamp(value, 0, maxHp);
            }
        }

        public int MP
        {
            get => mp;
            set
            {
                mp = Mathf.Clamp(value, 0, maxMp);
            }
        }

        public int MAXHP
        {
            get => maxHp;
            set
            {
                maxHp = Mathf.Clamp(value, 0, MAX_POSSIBLE_HP);
            }
        }

        public int MAXMP
        {
            get => maxMp;
            set
            {
                maxMp = Mathf.Clamp(value, 0, MAX_POSSIBLE_MP);
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
            Stats result = new Stats();

            //Null Check
            if (a == null && b == null)
                return result;
            else if (a == null)
                return b;
            else if (b == null)
                return a;


            result.lv = a.lv + b.lv;
            result.exp = a.exp + b.exp;
            result.nxtExp = a.nxtExp + b.nxtExp;
            result.hp = a.hp + b.hp;
            result.maxHp = a.maxHp + b.maxHp;
            result.mp = a.mp + b.mp;
            result.maxMp = a.maxMp + b.maxMp;
            result.atk = a.atk + b.atk;
            result.matk = a.matk + b.matk;
            result.def = a.def + b.def;
            result.mdef = a.mdef + b.mdef;
            result.spd = a.spd + b.spd;
            result.evs = a.evs + b.evs;
            result.enc = a.enc + b.enc;

            // Return the new Stats object
            return result;
        }

    }
}