

using System.Collections;
using UnityEngine;

namespace Core
{

    public class CutsceneManager : MonoBehaviour
    {

        public bool tryPlayCutscene(Cutscene scene)
        {

            if (Game.manager.State != GameState.World)
                return false;

            Game.manager.changeState(GameState.Cutscene);
            StartCoroutine(CO_PlayCutscene(scene));

            return true;
        }

        private IEnumerator CO_PlayCutscene(Cutscene scene)
        {

            foreach (ICutCommand command in scene.Commands)
            {
                StartCoroutine(command.CO_Execute());

                while (!command.isFinished)
                    yield return null;
            }

            scene.IsFinsihed = true;

            Game.manager.returnState();
            yield return null;
        }
    }
}

