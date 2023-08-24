using Core;
using UnityEngine;

namespace Battle
{
    public class BattleWindow : MonoBehaviour
    {
        [SerializeField] private GameObject mainSelectionWindow;
        [SerializeField] private GameObject ItemWindow;
        [SerializeField] private GameObject SkillWindow;

        private ItemMenu itemMenu;


        private void Awake()
        {
            itemMenu = ItemWindow.GetComponent<ItemMenu>();
        }

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


    }
}