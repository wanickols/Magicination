

using System.Collections;
using UnityEngine;

namespace Core
{

    public class CutsceneManager : MonoBehaviour
    {
        private GameState prevState = GameState.World; //Use this instead of gamestate return so can go between cutscene and dialogue in both world and battles. 

        public bool tryPlayCutscene(Cutscene scene)
        {

            if (Game.manager.State != GameState.World) // Or Battle someday
                return false;

            prevState = Game.manager.State;

            Game.manager.changeState(GameState.Cutscene);
            StartCoroutine(CO_PlayCutscene(scene));

            return true;
        }

        private IEnumerator CO_PlayCutscene(Cutscene scene)
        {

            foreach (ICutCommand command in scene.Commands)
            {
                StartCoroutine(command.CO_Execute());

                yield return null;
                while (!command.isFinished)
                    yield return null;
            }

            scene.IsFinsihed = true;

            Game.manager.changeState(prevState);
            yield return null;
        }
    }
}

