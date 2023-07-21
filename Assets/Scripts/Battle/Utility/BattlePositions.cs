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

            one = new Vector2(-3, -.85f);
            two = new Vector2(-3.5f, -1f);
            three = new Vector2(-3, -.7f);
            four = new Vector2(-4f, -1.1f);

            //List of one player
            oneSpawnPositions.Add(one); // -3, -.85f

            //List of two players
            twoSpawnPositions.Add(three); // -3, -.7
            twoSpawnPositions.Add(two);  //-3.5, -1f

            //List of three players
            one.y = -.5f;
            two.y = -.8f;

            threeSpawnPositions.Add(one); //-3, -.5f 
            threeSpawnPositions.Add(two); // -3.5, -.8f
            threeSpawnPositions.Add(four); // -4, -1.1f

            //List of four players

            two.y = -.7f;
            three = new Vector2(-4, -.9f);
            four.x = 4.5f;

            fourSpawnPositions.Add(one); //-3, -.5f 
            fourSpawnPositions.Add(two); // -3.5, -.8f
            fourSpawnPositions.Add(three);
            fourSpawnPositions.Add(four);
        }


    }

    public class EnemyBattlePositions : BattlePositions
    {


        public EnemyBattlePositions() : base() { }

        protected override void CreateLists()
        {
            one = new Vector2(3f, -.85f);

            //List of one player
            oneSpawnPositions.Add(one); // 3, -.85f

            three = new Vector2(3f, -.7f);
            two = new Vector2(3.5f, -1f);

            //List of two players
            twoSpawnPositions.Add(three); // 3, .7
            twoSpawnPositions.Add(two);  //3.5, -1

            //List of three players
            one.y = -.5f;
            two.y = -.8f;
            three = new Vector2(4f, -1.1f);

            threeSpawnPositions.Add(one); // 3, -.5
            threeSpawnPositions.Add(two); // 3.5, -.8
            threeSpawnPositions.Add(three); // 4, -1.1

            //List of four players

            two.y = -.7f;
            three.y = -.9f;
            four = new Vector2(4.5f, -1.1f);

            fourSpawnPositions.Add(one); // 3, -.5
            fourSpawnPositions.Add(two); // 2.9, .75
            fourSpawnPositions.Add(three); // 3.65, -.75
            fourSpawnPositions.Add(four);  // 3.65, -.25 
        }
    }
}
