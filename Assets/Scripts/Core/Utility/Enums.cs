namespace Core
{

    //Directions in Game
    public enum Dir
    {
        Up,
        Down,
        Left,
        Right
    }


    //States of the game
    public enum GameState
    {
        World,
        Cutscene,
        Dialogue,
        Battle,
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
    //Spawn Counts for Players and Enemy Spawning
    public enum SpawnCounts
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    public enum EnemyRarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary
    }
}
