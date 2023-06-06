using UnityEngine;

[System.Serializable]
public class Stats
{
    private const int MAX_POSSIBLE_HP = 9999; //Health Points
    private const int MAX_POSSIBLE_MP = 9999; //Magic Points

    private const int MAX_POSSIBLE_ATK = 999; //Physical Attack
    private const int MAX_POSSIBLE_MATK = 999; //Magic Attack

    private const int MAX_POSSIBLE_DEF = 999; //Physical Defense
    private const int MAX_POSSIBLE_MDEF = 999; //Magic Defense

    private const int MAX_POSSIBLE_SPD = 99; //Speed 
    private const int MAX_POSSIBLE_EVS = 99; //Evasion


    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int mp;
    [SerializeField] private int maxMp;
    [SerializeField] private int atk;
    [SerializeField] private int matk;
    [SerializeField] private int def;
    [SerializeField] private int mdef;
    [SerializeField] private int spd;
    [SerializeField] private int evs;

    public Stats(int hp, int maxHp, int mp, int maxMp, int atk, int matk, int def, int mdef, int spd, int evs)
    {
        this.hp = hp;
        this.maxHp = maxHp;
        this.maxMp = maxMp;
        this.mp = mp;
        this.atk = atk;
        this.matk = matk;
        this.def = def;
        this.spd = spd;
        this.evs = evs;
    }



    public int Initative => SPD + Random.Range(-1, 2); //Battle order determinant


    public int HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, MAX_POSSIBLE_HP);
        }
    }

    public int MP
    {
        get => mp;
        set
        {
            mp = Mathf.Clamp(value, 0, MAX_POSSIBLE_MP);
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
}
