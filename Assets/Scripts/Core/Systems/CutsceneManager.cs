

using System.Collections;

namespace MGCNTN.Core
{

    public class CutsceneManager
    {
        private StateManager stateManager;
        public Map map => Game.manager.mapManager.map;

        public CutsceneManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }


        public bool tryPlayCutscene(Cutscene scene)
        {

            if (!stateManager.tryState(GameState.Cutscene))
                return false;

            Game.manager.StartCoroutine(CO_PlayCutscene(scene));

            return true;
        }

        private IEnumerator CO_PlayCutscene(Cutscene scene)
        {

            foreach (ICutCommand command in scene.Commands)
            {
                Game.manager.StartCoroutine(command.CO_Execute());

                yield return null;
                while (!command.isFinished)
                    yield return null;
            }

            scene.IsFinsihed = true;

            stateManager.restorePreviousState();
            yield return null;
        }
    }
}

