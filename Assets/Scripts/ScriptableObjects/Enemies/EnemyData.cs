using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject actorPrefab;

    public GameObject ActorPrefab => actorPrefab;


}
