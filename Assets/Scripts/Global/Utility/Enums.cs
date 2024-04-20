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
        Stats,
        Status,
    }

    public enum StatusType
    {

        Burn,
        Confused,
        Paralysis,
        Poison,
        Petrified,
        Sleep,
        Slowed,
        //TODO
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

    public enum ItemMenuAction
    {
        use,
        sort,
        key,
        //discard?
    }
    //Skills
    public enum SkillStatus
    {
        unlocked,
        locked,
        hidden
    }


    //Classes
    public enum BaseClasses
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

    //Characters
    public enum PlayableCharacters
    {
        Aurora,
        Leon,
        Seth,
        Zane
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
        Tree,
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
            SkillCateogrySelection,
            SkillSelection,
            SkillActionSelection,
            SkillCombinationSelection,
        }

        public enum PartyTargetSelections
        {
            item,
            skill,
        }

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
