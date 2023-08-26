using UnityEngine;

namespace MGCNTN.Battle
{
    public class BattleWindow : MonoBehaviour
    {
        /// Private parameters
        [SerializeField] private GameObject mainSelectionWindow;
        [SerializeField] private GameObject ItemWindow;
        [SerializeField] private GameObject SkillWindow;

        private ItemMenu itemMenu;

        /// Unity Functions
        private void Awake()
        {
            itemMenu = ItemWindow.GetComponent<ItemMenu>();
        }

        /// Public Functions
        public void ShowItemWindow()
        {
            itemMenu.initItems();
            mainSelectionWindow.SetActive(false);
            ItemWindow.SetActive(true);
        }

        public void closeItemWindow()
        {
            itemMenu.clearItems();
            mainSelectionWindow.SetActive(true);
            ItemWindow.SetActive(false);
        }

        public Consumable getItem(int index) => itemMenu.selectItem(index);

    }
}