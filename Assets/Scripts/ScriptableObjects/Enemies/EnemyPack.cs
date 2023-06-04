using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Pack", menuName = "New Enemy Pack")]
public class EnemyPack : ScriptableObject
{
    [SerializeField] private List<EnemyData> data;
    [SerializeField] private List<float> xSpawnCoordinates;
    [SerializeField] private List<float> ySpawnCoordinates;

    public IReadOnlyList<EnemyData> Data => data;
    public IReadOnlyList<float> XSpawnCoordinates => xSpawnCoordinates;
    public IReadOnlyList<float> YSpawnCoordinates => ySpawnCoordinates;
}
