using UnityEngine;

[CreateAssetMenu(fileName = "New Party Member", menuName = "New Party Member")]
public class PartyMember : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private Stats stats;
    [SerializeField] private Sprite portrait;

    public string Name => _name;
    public GameObject ActorPrefab => actorPrefab;
    public Stats Stats => stats;
    public Sprite Portrait => portrait;

}
