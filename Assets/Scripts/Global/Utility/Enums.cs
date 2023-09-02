namespace MGCNTN
{
    //Directions in Game
    public enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }

    /// Global Enums
    //Augemenations
    public enum AugType
    {
        LVL,
        HP,
        EXP,
        MP,
        ATK,
        DEF,
        MATK,
        MDEF,
        EVS,
        SPD,
        ENC,
        ACC,
    }

    //Items
    public enum ItemRarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary
    }

    public enum EquippableType
    {
        Weapon,
        Head,
        Arms,
        Chest,
        Legs,
        Accesesory,
    }

    public enum equipmentEffect
    {
        none,
        loot,
        encounter,
    }

    public enum itemMenuAction
    {
        use,
        sort,
        key,
        //discard?
    }
    //Skills

    //Classes
    public enum baseClasses
    {
        //Physical
        Warrior,
        Rogue,
        Ranger,

        //Magic
        Mage,
        Cleric,
        Bard,
    }

    //Rarity for Enemy spawns and stats
    public enum EnemyRarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary
    }

    //UI
    public enum SelectorType
    {
        Vertical,
        Horizontal,
        Grid,
    }

    //Triggers types for cutscenes and other events
    public enum TriggerType
    {
        Auto,
        Touch,
        Call
    }

    ///Core Enums
    namespace Core
    {
        //States of the game
        public enum GameState
        {
            World,
            Cutscene,
            Dialogue,
            Battle,
            Transition,
            Menu
        }

        //Danger Level of a Region
        public enum DangerLevel
        {
            low = 1,
            medium = 2,
            high = 3,
            extreme = 4,
        }

        //UI
        public enum mainSelections
        {
            Items,
            Skills,
            Equip,
            Status,
            Order,
            Save,
            Quit
        }

        public enum MenuState
        {
            Main,
            MemberSelection,
            PartyTargetSelection,

            //Equip
            EquipmentSelection,
            EquippableSelection,

            //Item
            ItemActionSelection,
            ItemSelection,

            //Skill
            SkillActionSelection,
            SkillSelection
        }

        public enum PartyTargetSelections
        {
            item,
            skill,
        }
    }

    namespace Battle
    {

        //Actors
        public enum ActorType
        {
            Ally,
            Enemy
        }

        //Spawn Counts for Players and Enemy Spawning
        public enum SpawnCounts
        {
            None,
            One,
            Two,
            Three,
            Four,
        }

        //States for Battle
        public enum BattleStates
        {
            battle,
            select
        };

        public enum BattleMenuStates
        {
            mainSelection,
            itemSelection,
            skillSelection
        }

        public enum BattleMainSelections
        {
            Attack,
            Skills,
            Items,
            Run
        }

    }
}
