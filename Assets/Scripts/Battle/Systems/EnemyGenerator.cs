using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class EnemyGenerator
    {
        //Parameters
        private EnemyPack enemyPack;
        private float enemyAddPercentage = .15f; //This should probably move
        private Region region;
        private EnemyBattlePositions battlePositions = new EnemyBattlePositions();


        /// Public
        //Functions
        public EnemyGenerator(Region region) => this.region = region;


        public KeyValuePair<EnemyPack, List<Actor>> generate(TurnSystem turnSystem)
        {
            generateEnemies();
            List<Actor> enemies = new List<Actor>();

            for (int i = 0; i < enemyPack.Enemies.Count; i++)
            {
                EnemyData enemyData = GameObject.Instantiate(enemyPack.Enemies[i]); // Should keep fron saving between runs.
                Vector2 spawnPos = new Vector2(enemyPack.SpawnCoordinates[i].x, enemyPack.SpawnCoordinates[i].y);

                Actor enemy = GameObject.Instantiate(enemyData.ActorPrefab, spawnPos, Quaternion.identity).GetComponent<Actor>();
                enemy.setMemberBattleInfo(enemyData.Stats, enemyData.MenuPortrait);

                turnSystem.enqueue(enemy);
                enemies.Add(enemy);
            }


            KeyValuePair<EnemyPack, List<Actor>> enemyPair = new KeyValuePair<EnemyPack, List<Actor>>(enemyPack, enemies);

            return enemyPair;

        }

        /// Private
        private void generateEnemies()
        {
            enemyPack = new EnemyPack();
            List<EnemyData> enemies = getRarityList();

            //Check for errors
            enemies = checkEnemiesList(enemies);

            if (enemies.Count == 0)
            {
                return;
            }


            int minWeight = region.getLowestWeight();

            int currWeight = 0;
            //While current weight is less than total allowed weight minus smallest weight in group of enemies
            while (currWeight <= (region.maxWeight - minWeight))
            {
                if (currWeight != 0)
                {
                    float val = Random.value;
                    if (val > (1 - enemyAddPercentage)) //generates float between 0 and 1 so should be 85%
                        break;
                }

                int selectedEnemy = Random.Range(0, enemies.Count); // returns a number between 0 and total enemies in region

                EnemyData e = enemies[selectedEnemy];

                if (e.Stats.BattleWeight + currWeight <= region.maxWeight)
                {
                    enemyPack.addEnemy(e);
                    currWeight += e.Stats.BattleWeight;
                }
            }

            setPostions();
        }
        private List<EnemyData> getRarityList()
        {
            int rarityChance = Random.Range(0, 100);
            int cumulative = 0;
            for (int i = 0; i < region.probabilities.Count; i++)
            {
                cumulative += region.probabilities[i].probability;

                // Check if the number is less than or equal to the cumulative probability
                if (rarityChance <= cumulative)
                {
                    Debug.Log((EnemyRarity)i);
                    // Return the corresponding list of enemies based on the index
                    switch (i)
                    {
                        case 0:
                            return region.commonEnemies;
                        case 1:
                            return region.uncommonEnemies;
                        case 2:
                            return region.rareEnemies;
                        case 3:
                            return region.epicEnemies;
                        case 4:
                            return region.legendaryEnemies;
                        default:
                            return region.commonEnemies; // default case, should not happen
                    }
                }
            }
            // If the loop ends without returning, return the common enemies list as a fallback
            return region.commonEnemies;
        }
        private List<EnemyData> checkEnemiesList(List<EnemyData> enemies)
        {
            int i = 0;
            while (enemies.Count == 0)
            {
                switch (i)
                {
                    case 0:
                        enemies = region.commonEnemies;
                        break;
                    case 1:
                        enemies = region.uncommonEnemies;
                        break;
                    case 2:
                        enemies = region.rareEnemies;
                        break;
                    case 3:
                        enemies = region.epicEnemies;
                        break;
                    case 4:
                        enemies = region.legendaryEnemies;
                        break;
                    default:
                        enemies = region.commonEnemies; // default case, should not happen
                        Debug.LogError("No enemies in region specified");
                        return enemies;
                }
                i++;
            }
            return enemies;
        }
        private void setPostions()
        {
            SpawnCounts numEnemies = (SpawnCounts)enemyPack.Enemies.Count;

            int i = 0;

            List<Vector2> pos = battlePositions.getPositions(numEnemies);

            foreach (EnemyData e in enemyPack.Enemies)
            {
                enemyPack.setCoordinates(pos[i]);
                i++;
            }
        }
    }
}