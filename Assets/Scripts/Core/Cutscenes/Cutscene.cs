using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Cutscene : MonoBehaviour
    {


        [SerializeField] private bool autoplay = false;
        private bool isStarted = false;
        private bool isFinished = false;

        [SerializeReference]
        private List<ICutCommand> commands = new List<ICutCommand>();
        CutsceneManager manager;

        public IReadOnlyList<ICutCommand> Commands => commands;

        public bool IsFinsihed
        {
            get => isFinished;
            set
            {
                isFinished = value;
                if (value == true)
                    Destroy(this.gameObject);

            }
        }

        private void Start()
        {
            manager = Game.manager.cutsceneManager;

        }

        private void Update()
        {
            if (autoplay && !isStarted)
                Play();
        }


        private void Play()
        {
            isStarted = manager.tryPlayCutscene(this);
        }

        public void InsertCommand(int index, ICutCommand command) => commands.Insert(index, command);

    }
}
