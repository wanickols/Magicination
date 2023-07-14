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
        none,
        low,
        medium,
        high,
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
