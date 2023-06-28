using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Core
{
    public class SceneLoader : MonoBehaviour
    {
        public enum scene
        {
            main,
            battle,
        };


        public static scene savedScene;
        private static Vector2 savedPlayerLocation;
        private Player player;
        private Map map;
        private int currIndex = 0;

        public SceneLoader(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        public IEnumerator loadScene(scene scene, GameObject transitionPrefab)
        {

            Animator animator = playAnimation(transitionPrefab);
            while (animator.IsAnimating()) yield return null;
            loadScene(scene);
        }

        private Animator playAnimation(GameObject transitionPrefab)
        {
            return Instantiate(transitionPrefab, player.transform.position, Quaternion.identity).GetComponent<Animator>();
        }


        public void loadScene(scene scene)
        {
            switch (scene)
            {
                case scene.main:
                    currIndex = 0;
                    loadMainScene();
                    break;
                case scene.battle:
                    currIndex = 1;
                    loadBattleScene();
                    break;
                default:
                    Debug.Log("No Scene Found");
                    return;
            }

            SceneManager.LoadScene(currIndex);
        }
        private void loadMainScene()
        {
            SceneManager.sceneLoaded += restorePlayerPositonAndGameObject;
        }

        private void loadBattleScene()
        {
            //Save current
            savedScene = (scene)SceneManager.GetActiveScene().buildIndex;
            savedPlayerLocation = map.grid.Center2D(player.currCell);

            // Player
            player.gameObject.SetActive(false);
        }


        private void restorePlayerPositonAndGameObject(Scene scene, LoadSceneMode mode)
        {

            player.transform.position = savedPlayerLocation;
            player.gameObject.SetActive(true);
            SceneManager.sceneLoaded -= restorePlayerPositonAndGameObject;
        }
    }
}
