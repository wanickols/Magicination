using UnityEngine;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private GameObject partMemberInfoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePartyMemberInfo();
    }

    private void GeneratePartyMemberInfo()
    {
        foreach (PartyMember member in Party.ActiveMembers)
        {
            if (this.gameObject.transform.childCount < 4)
            {
                Instantiate(partMemberInfoPrefab, this.gameObject.transform);
            }
        }
    }
}
