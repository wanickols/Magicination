using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {
        // Start is called before the first frame update

        void Start()
        {
            ShowDefaultView();
        }

        public void ShowDefaultView()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }
        public void ShowEquipmentView(int selected)
        {
            PartyMember selectedMember = Party.ActiveMembers[selected];
            //Show Equipment
            Debug.Log(selectedMember.name);
        }
    }
}
