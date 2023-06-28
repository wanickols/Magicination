using System.Collections.Generic;
using UnityEngine;

public class BattlePositions
{

    private const int numPositions = 4; //For all possible battle positions;
    private List<Vector2> onePlayerSpawnPositions = new List<Vector2>();
    private List<Vector2> twoPlayerSpawnPositions = new List<Vector2>();
    private List<Vector2> threePlayerSpawnPositions = new List<Vector2>();
    private List<Vector2> fourPlayerSpawnPositions = new List<Vector2>();

    public enum SpawnCounts
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    private Dictionary<SpawnCounts, List<Vector2>> spawnPositions = new Dictionary<SpawnCounts, List<Vector2>>();


    public List<Vector2> getPositions(SpawnCounts numPositions)
    {
        if (spawnPositions.ContainsKey(numPositions))
            return spawnPositions[numPositions];
        else
        {
            Debug.LogError("Could not find a position for numPositions in BattlePositions");
            return new List<Vector2>();
        }
    }


    public BattlePositions()
    {
        CreateLists();
        CreateDictionary();
    }


    private void CreateLists()
    {
        Vector2 one = new Vector2(-4, 0);
        Vector2 two = new Vector2(-4, .6f);
        Vector2 three = new Vector2(-4, -.6f);
        Vector2 four = new Vector2(-4, .25f);

        //List of one player
        onePlayerSpawnPositions.Add(one); // -4, 0

        //List of two players
        twoPlayerSpawnPositions.Add(two); // -4, .6
        twoPlayerSpawnPositions.Add(three);  //-4, -.6

        //List of three players
        one.x = -3;

        threePlayerSpawnPositions.Add(one); //-3.0 
        threePlayerSpawnPositions.Add(two); // -4, .6
        threePlayerSpawnPositions.Add(three); // -4, -.6

        //List of four players
        one = new Vector2(-3.25f, -.25f);
        two = new Vector2(-3.25f, .75f);
        three = new Vector2(-4, -.75f);

        fourPlayerSpawnPositions.Add(one);
        fourPlayerSpawnPositions.Add(two);
        fourPlayerSpawnPositions.Add(three);
        fourPlayerSpawnPositions.Add(four);
    }

    private void CreateDictionary()
    {


        //Create Dictionary
        spawnPositions.Add(SpawnCounts.One, onePlayerSpawnPositions);
        spawnPositions.Add(SpawnCounts.Two, twoPlayerSpawnPositions);
        spawnPositions.Add(SpawnCounts.Three, threePlayerSpawnPositions);
        spawnPositions.Add(SpawnCounts.Four, fourPlayerSpawnPositions);
    }
}
