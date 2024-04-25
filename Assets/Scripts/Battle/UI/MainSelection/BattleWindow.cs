using UnityEngine;

namespace MGCNTN.Battle
{
    public class BattleWindow : MonoBehaviour
    {
        /// Private parameters
        [SerializeField] private GameObject mainSelectionWindow;
        [SerializeField] private GameObject ItemWindow;
        [SerializeField] private GameObject SkillWindow;
        [SerializeField] private BattleData data;

        private ItemMenu itemMenu;
        private SkillMenu skillMenu;

        /// Unity Functions
        private void Awake()
        {
            itemMenu = ItemWindow.GetComponent<ItemMenu>();
            skillMenu = SkillWindow.GetComponent<SkillMenu>();
            skillMenu.setBattleData(data);
        }

        /// Public Functions
        public void ShowItemWindow()
        {
            mainSelectionWindow.SetActive(false);
            ItemWindow.SetActive(true);
        }

        public void closeItemWindow()
        {
            mainSelectionWindow.SetActive(true);
            ItemWindow.SetActive(false);
        }

        /// Public Functions
        public void ShowSkillWindow()
        {
            mainSelectionWindow.SetActive(false);
            SkillWindow.SetActive(true);
        }

        public void closeSkillWindow()
        {
            mainSelectionWindow.SetActive(true);
            SkillWindow.SetActive(false);
        }

        public void UpdateSkills() => skillMenu.initSkills();
        public Consumable getItem(int index) => itemMenu.selectItem(index);
        public Skill getSkill(int index) => skillMenu.selectSkill(index);

    }
}