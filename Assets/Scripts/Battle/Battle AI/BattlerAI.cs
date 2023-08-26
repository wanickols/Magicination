using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public abstract class BattlerAI : MonoBehaviour
    {
        protected Actor actor;
        protected List<Actor> possibleTargets = new List<Actor>();

        protected void Awake() => actor = GetComponent<Actor>();

        public void updateTargets(List<Actor> targets) => possibleTargets = targets;

        public abstract ICommand ChooseAction();
    }
}
