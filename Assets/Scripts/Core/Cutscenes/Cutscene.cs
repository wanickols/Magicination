using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cutscene : MonoBehaviour, ITriggerByTouch
    {


        [SerializeField] private TriggerType trigger = TriggerType.Auto;
        [SerializeField] private bool callOnce = false;

        private bool isStarted = false;
        private bool isFinished = false;

        [SerializeReference]
        private List<ICutCommand> commands = new List<ICutCommand>();
        CutsceneManager manager;

        public IReadOnlyList<ICutCommand> Commands => commands;

        public Vector2Int Cell => Game.manager.Map.grid.GetCell2D(gameObject);

        public bool IsFinsihed
        {
            get => isFinished;
            set
            {
                isFinished = value;
                if (value == true && callOnce)
                    Destroy(this.gameObject);

            }
        }

        private void Start()
        {
            manager = Game.manager.cutsceneManager;
            if (trigger == TriggerType.Touch)
                Game.manager.Map.Triggers.Add(Cell, this); //adds to map
        }

        private void Update()
        {
            if (trigger == TriggerType.Auto && !isStarted)
                Play();
        }

        public void Trigger()
        {
            Play();
        }

        private void Play()
        {
            isStarted = manager.tryPlayCutscene(this);
        }

        private void OnDestroy()
        {
            Game.manager.Map.Triggers.Remove(Cell);
        }

        public void InsertCommand(int index, ICutCommand command) => commands.Insert(index, command);
        public void RemoveAt(int i) => commands.RemoveAt(i);
        public void SwapCommands(int i, int j) => (commands[i], commands[j]) = (commands[j], commands[i]);

    }
}
