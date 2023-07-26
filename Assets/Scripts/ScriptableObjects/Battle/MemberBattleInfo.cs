using UnityEngine;


[CreateAssetMenu(fileName = "Member Battle Info", menuName = "Member Battle Info")]
public class MemberBattleInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject actorPrefab;

    [SerializeField] private Sprite menuPortrait;

    public string Name => _name;
    public GameObject ActorPrefab => actorPrefab;

    public Sprite MenuPortrait => menuPortrait;
}
