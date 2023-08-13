using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {

        //Party Info
        [SerializeField] private GameObject partMemberInfoPrefab;

        // Start is called before the first frame update

        void Start()
        {
            ShowDefaultView();
        }

        public void ShowDefaultView()
        {
            foreach (PartyMember member in Party.ActiveMembers)
            {
                if (this.gameObject.transform.childCount < 4)
                {
                    Instantiate(partMemberInfoPrefab, this.gameObject.transform);
                }
            }
        }
        public void ShowEquipmentView(PartyMember member)
        {
            //Show Equipment
        }
    }
}
