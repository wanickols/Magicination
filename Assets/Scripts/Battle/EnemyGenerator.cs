using Battle;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator
{
    private EnemyPack enemyPack;
    private float enemyAddPercentage = .15f; //This should probably move
    private Region region;
    private EnemyBattlePositions battlePositions = new EnemyBattlePositions();
    public EnemyGenerator(Region region) => this.region = region;

    public EnemyPack generateEnemies()
    {
        enemyPack = new EnemyPack();
        List<EnemyData> enemies = getRarityList();
        if (enemies.Count == 0)
            enemies = region.commonEnemies;

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

        return enemyPack;
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
