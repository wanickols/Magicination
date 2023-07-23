using UnityEngine;

namespace Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject StatsContainer;
        [SerializeField] private GameObject PartymemberPrefab;

        public TurnBar turnBar { get; private set; }

        private void Awake()
        {
            turnBar = FindObjectOfType<TurnBar>();
        }

        public void AddPartyMemberUI(PartyMember member)
        {
            GameObject partyMem = Instantiate(PartymemberPrefab, StatsContainer.transform);

            StatContainer stat = partyMem.GetComponent<StatContainer>();

            stat.DisplayInfo(member);

        }
    }
}
