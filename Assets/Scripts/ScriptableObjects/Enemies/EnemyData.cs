using Battle;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private EnemyStats stats;

    public GameObject ActorPrefab => actorPrefab;
    public EnemyStats Stats => stats;
}
