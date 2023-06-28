using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{

    private static int battleSceneBuildIndex = 1;
    private static int savedSceneBuildIndex;
    private static Vector2 savedPlayerLocation;
    public static Player player;


    public static void loadBattleScene()
    {
        //Save current
        savedSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        savedPlayerLocation = player.currCell.Center2D();

        // Player
        player.gameObject.SetActive(false);

        //Scene transition
        SceneManager.LoadScene(battleSceneBuildIndex);
    }

    public static void reloadSavedScene()
    {
        SceneManager.sceneLoaded += restorePlayerPositonAndGameObject;
        SceneManager.LoadScene(savedSceneBuildIndex);
    }

    public static void restorePlayerPositonAndGameObject(Scene scene, LoadSceneMode mode)
    {

        player.transform.position = savedPlayerLocation;
        player.gameObject.SetActive(true);
        SceneManager.sceneLoaded -= restorePlayerPositonAndGameObject;
    }
}
