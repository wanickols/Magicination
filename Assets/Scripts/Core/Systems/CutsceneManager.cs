

using System.Collections;

namespace Core
{

    public class CutsceneManager
    {
        private GameState prevState = GameState.World; //Use this instead of gamestate return so can go between cutscene and dialogue in both world and battles. 

        private StateManager stateManager;
        public Map map => Game.manager.mapManager.map;

        public CutsceneManager(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }


        public bool tryPlayCutscene(Cutscene scene)
        {

            if (stateManager.State != GameState.World) // Or Battle someday
                return false;

            prevState = stateManager.State;

            stateManager.changeState(GameState.Cutscene);
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

            stateManager.changeState(prevState);
            yield return null;
        }
    }
}

