using Battle;
using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Region", menuName = "New Region")]
public class Region : ScriptableObject
{
    [SerializeField] public int maxWeight;

    [SerializeField] public List<EnemyData> possibleEnemies;
    [SerializeField] public DangerLevel dangerLevel = DangerLevel.low;

    [SerializeField] public List<RarityProbability> probabilities;


    [NonSerialized]
    public List<EnemyData> commonEnemies = new List<EnemyData>(),
        uncommonEnemies = new List<EnemyData>(), rareEnemies = new List<EnemyData>(),
        epicEnemies = new List<EnemyData>(), legendaryEnemies = new List<EnemyData>();


    public event Action TriggerBattle;

    private void OnEnable()
    {
        initLowestWeight();
        initRarityLists();
    }

    private int lowestWeight = -1;

    private void initLowestWeight()
    {
        if (lowestWeight == -1)
        {
            lowestWeight = possibleEnemies[0].Stats.BattleWeight;
            foreach (EnemyData e in possibleEnemies)
            {
                if (e.Stats.BattleWeight < lowestWeight)
                    lowestWeight = e.Stats.BattleWeight;
            }
        }
    }

    private void initRarityLists()
    {
        foreach (EnemyData e in possibleEnemies)
        {
            switch (e.Stats.enemyRarity)
            {
                case EnemyRarity.common:
                    commonEnemies.Add(e);
                    break;
                case EnemyRarity.uncommon:
                    uncommonEnemies.Add(e);
                    break;
                case EnemyRarity.rare:
                    rareEnemies.Add(e);
                    break;
                case EnemyRarity.epic:
                    epicEnemies.Add(e);
                    break;
                case EnemyRarity.legendary:
                    legendaryEnemies.Add(e);
                    break;
            }
        }

    }


    public int getLowestWeight()
    {
        initLowestWeight();
        return lowestWeight;
    }

    public void tryRandomEncounter()
    {
        int chance = UnityEngine.Random.Range(0, 100);
        float enc = Party.getEncounterRate() * (float)((int)dangerLevel / 2); //Average party: Low = 1/20 medium 1/10 high 3/20 extreme 2/10

        if (chance < enc)
        {
            TriggerBattle?.Invoke();
        }

    }
    //Climate
}
