using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{

    public class SkillMenu : MonoBehaviour
    {
        ///Public Parameters

        public Skill skill => treeSelector.currNode.GetComponent<SkillHolder>().Skill;

        [NonSerialized] public PartyMember member;

        ///Private Parameters
        [SerializeField] private GameObject treesParent; //curently one, need to make more
        [SerializeField] private TreeSelector treeSelector;
        [SerializeField] private GameObject ActionBar;

        [SerializeField] private SkillHolder skill1, skill2, skill3;

        [SerializeField] private GameObject combinationVisible, combinationNotVisible;
        [SerializeField] private GameObject successWindow, failWindow;

        //Components
        private SkillTree tree = new SkillTree();
        private ActionBar actionBar;
        private List<GameObject> treeParents = new List<GameObject>();



        private int currTree = 0;
        public bool canSelect => skill.Data.skillStatus == SkillStatus.unlocked;

        public int skillCount = 0;

        private bool clearNext = false;

        ///Unity Functions
        private void Awake()
        {
            foreach (Transform t in treesParent.GetComponentInChildren<Transform>())
            {
                if (t.GetComponent<Selector>())
                    continue;

                t.gameObject.SetActive(false);
                treeParents.Add(t.gameObject);

            }

            treeSelector.tree = tree;

            tree.reset(treeParents[currTree]);
            treeParents[currTree].SetActive(true);

            actionBar = GetComponentInChildren<ActionBar>();
            ActionBar.SetActive(false);
        }

        ///Public Functions

        public void selectTree(int i)
        {
            treeParents[currTree].SetActive(false);
            currTree = i;
            tree.reset(treeParents[i]);
            treeParents[currTree].SetActive(true);
        }

        public string showDescription()
        {
            if (!skill)
                return "Error";


            if (skill.Data.skillStatus == SkillStatus.hidden)
                return "Hidden.";
            else
                return skill.Data.description;
        }
        public void openActionBar()
        {

            int mp = member.Stats.ENG;

            ActionBar.SetActive(true);

            if (!skill.Data.menuUsable || mp < skill.Data.cost)
                actionBar.deactivateOption(1);

        }
        public void closeActionBar() => ActionBar.SetActive(false);

        public bool selectSkill(int index)
        {
            if (index == 2)
                return combine();

            if (!skill)
                return false;

            changeSkills(skill, index);

            if (skillCount < 2)
                return false;



            return true;
        }

        private bool combine()
        {

            Skill skill = Game.manager.combinationManager.FindCombination(skill1.Skill.Data.displayName, skill2.Skill.Data.displayName);

            if (skill)
            {
                changeSkills(skill, 2);

                Debug.Log(skill.Data.description);
                skill.unlock();

                ShowSuccessWindow();
                clearNext = true;
                return false;
            }
            else
            {
                Debug.Log("Combination Failed");
                return true;
            }



        }

        public void back(int index)
        {
            if (clearNext)
            {
                skillCount = 0;
                skill1.Skill = null;
                skill2.Skill = null;
                skill3.Skill = null;
            }
            else
                removeSkill(index);
        }

        private void removeSkill(int index) => changeSkills(null, index);

        ///Private Functions
        ///
        private void changeSkills(Skill skill, int index)
        {
            switch (index)
            {
                case 0:
                    skill1.Skill = skill;
                    break;
                case 1:
                    skill2.Skill = skill;
                    break;
                case 2:
                    skill3.Skill = skill;
                    break;
            }

            if (skill)
                skillCount++;
            else
                skillCount--;

            checkCombine();
        }

        private void checkCombine()
        {
            bool active = skillCount == 2;
            combinationVisible.SetActive(active);
            combinationNotVisible.SetActive(!active);
        }

        private void ShowSuccessWindow() => successWindow.SetActive(true);

    }
}
