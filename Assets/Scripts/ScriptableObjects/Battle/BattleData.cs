using Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle Data", menuName = "Battle Data")]
public class BattleData : ScriptableObject
{

    // Later might set these lists to private sets so we can invoke these when changing externally.
    public Action<List<Actor>> updateAllies;
    public Action<List<Actor>> updateEnemies;

    [SerializeField] public Actor currentActor;

    [SerializeField] public List<Actor> allies = new List<Actor>();
    [SerializeField] public List<Actor> enemies = new List<Actor>();
    [SerializeField] public List<Actor> nextSix = new List<Actor>();

    public PriorityQueue<Actor, float> turnQueue = new PriorityQueue<Actor, float>(); // Use a priority queue to store the actors and their turn times


    [SerializeField] public EnemyPack enemyPack;

    public void setTargets()
    {
        updateAllies.Invoke(allies);
        updateEnemies.Invoke(enemies);
    }
}