using UnityEngine;


[CreateAssetMenu(fileName = "Battle Data", menuName = "Battle Data")]
public class BattleData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject actorPrefab;

    [SerializeField] private Sprite menuPortrait;

    public string Name => _name;
    public GameObject ActorPrefab => actorPrefab;

    public Sprite MenuPortrait => menuPortrait;
}
