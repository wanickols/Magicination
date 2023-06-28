using Battle;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private Stats stats;

    public GameObject ActorPrefab => actorPrefab;
    public Stats Stats => stats;
}
