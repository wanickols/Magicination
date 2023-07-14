using System.Collections.Generic;
using UnityEngine;
namespace Battle
{
    public abstract class BattlePositions
    {
        //private const int numPositions = 4; //For all possible battle positions;
        private Dictionary<SpawnCounts, List<Vector2>> spawnPositions = new Dictionary<SpawnCounts, List<Vector2>>();

        protected Vector2 one;
        protected Vector2 two;
        protected Vector2 three;
        protected Vector2 four;
        protected List<Vector2> oneSpawnPositions = new List<Vector2>();
        protected List<Vector2> twoSpawnPositions = new List<Vector2>();
        protected List<Vector2> threeSpawnPositions = new List<Vector2>();
        protected List<Vector2> fourSpawnPositions = new List<Vector2>();


        protected abstract void CreateLists();


        public BattlePositions()
        {
            CreateLists();
            CreateDictionary();
        }


        private void CreateDictionary()
        {
            //Create Dictionary
            spawnPositions.Add(SpawnCounts.One, oneSpawnPositions);
            spawnPositions.Add(SpawnCounts.Two, twoSpawnPositions);
            spawnPositions.Add(SpawnCounts.Three, threeSpawnPositions);
            spawnPositions.Add(SpawnCounts.Four, fourSpawnPositions);
        }

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

    }



    public class PlayerBattlePositions : BattlePositions
    {
        public PlayerBattlePositions() : base() { }

        protected override void CreateLists()
        {

            one = new Vector2(-4, 0);
            two = new Vector2(-4, .6f);
            three = new Vector2(-4, -.6f);
            four = new Vector2(-4, .25f);

            //List of one player
            oneSpawnPositions.Add(one); // -4, 0

            //List of two players
            twoSpawnPositions.Add(two); // -4, .6
            twoSpawnPositions.Add(three);  //-4, -.6

            //List of three players
            one.x = -3.25f;

            threeSpawnPositions.Add(one); //-3.25 
            threeSpawnPositions.Add(two); // -4, .6
            threeSpawnPositions.Add(three); // -4, -.6

            //List of four players
            one = new Vector2(-3.25f, -.25f);
            two = new Vector2(-3.25f, .75f);
            three = new Vector2(-4, -.75f);

            fourSpawnPositions.Add(one);
            fourSpawnPositions.Add(two);
            fourSpawnPositions.Add(three);
            fourSpawnPositions.Add(four);
        }


    }

    public class EnemyBattlePositions : BattlePositions
    {


        public EnemyBattlePositions() : base() { }

        protected override void CreateLists()
        {
            one = new Vector2(3.65f, 0);

            //List of one player
            oneSpawnPositions.Add(one); // 3.65, 0

            two = new Vector2(3.65f, .6f);
            three = new Vector2(3.65f, -.6f);

            //List of two players
            twoSpawnPositions.Add(two); // 3.65, .6
            twoSpawnPositions.Add(three);  //3.65, -.6

            //List of three players
            one.x = 2.65f;

            threeSpawnPositions.Add(one); // 2.65, 0
            threeSpawnPositions.Add(two); // 3.65, .6
            threeSpawnPositions.Add(three); // 3.65, -.6

            //List of four players
            one = new Vector2(2.9f, -.25f);
            two = new Vector2(2.9f, .75f);
            three = new Vector2(3.65f, -.75f);
            four = new Vector2(3.65f, .25f);

            fourSpawnPositions.Add(one); // 2.9, -.25
            fourSpawnPositions.Add(two); // 2.9, .75
            fourSpawnPositions.Add(three); // 3.65, -.75
            fourSpawnPositions.Add(four);  // 3.65, -.25 
        }
    }
}
