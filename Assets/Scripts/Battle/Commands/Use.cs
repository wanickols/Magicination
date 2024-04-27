using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class Use : ICommand
    {
        private Actor user;
        private List<Actor> targets;
        private ObjectData data;

        public Use(Actor user, List<Actor> targets, ObjectData data)
        {
            this.targets = targets;
            this.data = data;
            this.user = user;
        }

        public bool isFinished { get; private set; } = false;
        public IEnumerator Co_Execute()
        {
            foreach (var target in targets)
            {
                try
                {
                    BattleCalculations.useItem(user, target, data);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
            yield return null;
            isFinished = true;
        }
    }
}