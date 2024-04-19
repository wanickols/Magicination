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

        protected const int MAX_POSSIBLE_SPD = 99; //Speed 
        protected const int MAX_POSSIBLE_EVS = 99; //Evasion

        protected const int MAX_POSSIBLE_ENC = 100; //Encounter Rate


        //Actual stats
        [SerializeField] protected int lv;
        [SerializeField] protected int exp;
        [SerializeField] protected int nxtExp;
        [SerializeField] protected int hp;
        [SerializeField] protected int maxHp;
        [SerializeField] protected int eng;
        [SerializeField] protected int maxEng;
        [SerializeField] protected int atk;
        [SerializeField] protected int def;
        [SerializeField] protected int spd;
        [SerializeField] protected int evs;
        [SerializeField] protected int enc;


        /// Public Functions
        public Stats() => lv = exp = hp = maxHp = eng = maxEng = atk = def = spd = evs = 0;

        public Stats(int lv, int exp, int hp, int maxHp, int eng, int maxEng, int atk, int def, int spd, int evs)
        {
            this.lv = lv;
            this.exp = exp;
            this.hp = hp;
            this.maxHp = maxHp;
            this.maxEng = maxEng;
            this.eng = eng;
            this.atk = atk;
            this.def = def;
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

        public int ENG
        {
            get => eng;
            set
            {
                eng = Mathf.Clamp(value, 0, maxEng);
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

        public int DEF
        {
            get => def;
            set
            {
                def = Mathf.Clamp(value, 0, MAX_POSSIBLE_DEF);
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
            result.HP = a.hp + b.hp;
            result.MAXENG = a.maxEng + b.maxEng;
            result.ENG = a.eng + b.eng;
            result.ATK = a.atk + b.atk;
            result.DEF = a.def + b.def;
            result.SPD = a.spd + b.spd;
            result.EVS = a.evs + b.evs;
            result.ENC = a.enc + b.enc;

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
            result.HP = a.hp - b.hp;
            result.MAXENG = a.maxEng + b.maxEng;
            result.ENG = a.eng - b.eng;
            result.ATK = a.atk - b.atk;
            result.DEF = a.def - b.def;
            result.SPD = a.spd - b.spd;
            result.EVS = a.evs - b.evs;
            result.ENC = a.enc - b.enc;

            return result;
        }
    }
}