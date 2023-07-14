using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Pack", menuName = "New Enemy Pack")]
public class EnemyPack : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemies;
    [SerializeField] private List<Vector2> spawnCoordinates;


    public EnemyPack(List<EnemyData> enemies, List<Vector2> spawnCoordinates)
    {
        this.enemies = enemies;
        this.spawnCoordinates = spawnCoordinates;
    }
    public EnemyPack()
    {
        enemies = new List<EnemyData>();
        spawnCoordinates = new List<Vector2>();
    }

    public IReadOnlyList<EnemyData> Enemies => enemies;
    public IReadOnlyList<Vector2> SpawnCoordinates => spawnCoordinates;


    public void addEnemy(EnemyData newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public void setCoordinates(Vector2 coord)
    {
        spawnCoordinates.Add(coord);
    }
}
