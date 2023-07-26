using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject StatsContainer;
        [SerializeField] private GameObject PartymemberPrefab;
        [SerializeField] private GameObject BattleMenu;


        private List<StatContainer> statsContainerList = new List<StatContainer>();



        public TurnBar turnBar { get; private set; }

        //Selecting
        public bool hasTarget = false;
        public List<Actor> targets { get; private set; } = new List<Actor>();

        private void Awake()
        {
            turnBar = FindObjectOfType<TurnBar>();
        }

        public StatContainer AddPartyMemberUI(PartyMember member)
        {
            GameObject partyMem = Instantiate(PartymemberPrefab, StatsContainer.transform);

            statsContainerList.Add(partyMem.GetComponent<StatContainer>());
            StatContainer stat = statsContainerList.Last();

            stat.DisplayInfo(member);

            return stat;

        }

        public void LinkListeners(Actor actor)
        {
            StatContainer stat = statsContainerList.Last();
            actor.updateHealth += stat.updateHealth;
            actor.updateMP += stat.updateMP;
        }

        public void setBattleMenu(bool active)
        {
            BattleMenu.SetActive(active);
        }

        /// Selector Logic

        private int currRow = 0;
        private int currCol = 0;
        private Actor currSelected = null;
        private bool handlingInput;


        public IEnumerator CO_SelctSingleTarget(List<Ally> allies, List<Enemy> enemies)
        {

            currSelected = enemies.FirstOrDefault();
            currSelected.selector.SetActive(true);
            yield return new WaitForSeconds(.1f);

            while (!hasTarget)
            {

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                    VerticalMove(allies, enemies, 1);

                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    VerticalMove(allies, enemies, -1);

                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                    HorizontalMove(allies, enemies, 1);

                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                    HorizontalMove(allies, enemies, -1);

                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    SelectSingleTarget();

                if (handlingInput)
                {
                    yield return new WaitForSeconds(.1f);
                    handlingInput = false;
                }
                else
                    yield return null;
            }

            hasTarget = false;
            yield return null;

        }

        private void SelectSingleTarget()
        {
            hasTarget = true;
            targets.Add(currSelected);
            currSelected.selector.SetActive(false);
        }

        private void VerticalMove(List<Ally> allies, List<Enemy> enemies, int change)
        {
            handlingInput = true;
            //Allies
            if (currRow == 0)
            {
                ChangeCol(change, allies.Count - 1);
                ToggleSelection(allies[currCol]);
                return;
            }

            //Enemies
            ChangeCol(change, enemies.Count - 1);
            ToggleSelection(enemies[currCol]);
        }
        private void HorizontalMove(List<Ally> allies, List<Enemy> enemies, int change)
        {

            handlingInput = true;
            if (currRow == 0)
            {
                currRow = 1;

                if (currCol > enemies.Count - 1)
                    currCol = enemies.Count - 1;

                ToggleSelection(enemies[currCol]);
            }
            else
            {
                currRow = 0;

                if (currCol > allies.Count - 1)
                    currCol = allies.Count - 1;

                ToggleSelection(allies[currCol]);
            }

        }

        private void ToggleSelection(Actor target)
        {
            currSelected.selector.SetActive(false);
            target.selector.SetActive(true);
            currSelected = target;
        }

        private void ChangeCol(int change, int maxCount)
        {
            currCol += change;

            if (currCol < 0)
                currCol = 0;
            else if (currCol > maxCount)
                currCol = maxCount;
        }


    }

}
