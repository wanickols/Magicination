using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class which handles Selection logic
/// </summary>
/// <remarks>
/// Has no real dependecies, uses list of actors. Actors are required to have a selector on them. 
/// </remarks>
namespace MGCNTN.Battle
{
    public class Selection
    {
        ///Actions
        public Action selectTarget;
        public Action cancelTarget;

        ///Public Parameters
        public bool hasTarget = false;
        public List<Actor> targets { get; private set; } = new List<Actor>();

        ///Private Paramaters
        private int currRow = 0;
        private int currCol = 0;
        private bool handlingInput;
        private Actor currSelected = null;
        private GameObject selector => currSelected.gfx.selector;

        private bool cancel = false;

        ///Public Functions
        public IEnumerator CO_SelectSingleTarget(BattleData data)
        {
            bool live = true;
            List<Actor> allies;
            List<Actor> enemies;
            if (live)
                allies = data.allies.getLive();
            else
                allies = data.allies.getDead();

            enemies = data.enemies;

            currSelected = enemies.FirstOrDefault();
            selector.SetActive(true);
            yield return new WaitForSeconds(.1f);

            while (!hasTarget && !cancel)
            {

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                    VerticalMove(allies, enemies, -1);

                else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    VerticalMove(allies, enemies, 1);

                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)
                    || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                    HorizontalMove(allies, enemies);

                else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    SelectSingleTarget();

                else if (Input.GetKeyDown(KeyCode.Escape))
                    revertSelection();

                if (handlingInput)
                {
                    yield return new WaitForSeconds(.1f);
                    handlingInput = false;
                }
                else
                    yield return null;
            }

            cancel = false;
            hasTarget = false;
            yield return null;

        }

        ///Private Functions
        private void SelectSingleTarget()
        {
            targets.Clear();
            hasTarget = true;
            targets.Add(currSelected);
            selector.SetActive(false);
            selectTarget?.Invoke();
        }
        private void VerticalMove(List<Actor> allies, List<Actor> enemies, int change)
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
        private void HorizontalMove(List<Actor> allies, List<Actor> enemies)
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
            selector.SetActive(false);
            target.gfx.selector.SetActive(true);
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

        private void revertSelection()
        {
            cancel = true;
            selector.SetActive(false);
            cancelTarget?.Invoke();
        }
    }
}