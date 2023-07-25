using UnityEngine;

namespace Battle
{
    public abstract class BattlerAI : MonoBehaviour
    {
        protected Actor actor;

        protected void Awake()
        {
            actor = GetComponent<Actor>();
        }

        public abstract ICommand ChooseAction();
    }
}
