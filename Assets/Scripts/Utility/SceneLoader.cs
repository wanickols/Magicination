using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static int debugSceneBuildIndex = 0;
    private static int battleSceneBuildIndex = 1;
    private static int savedSceneBuildIndex;
    private static Vector2 savedPlayerLocation;

    public static void loadBattleScene()
    {
        //Save current
        savedSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        savedPlayerLocation = Game.Player.currCell.Center2D();

        // Player
        Game.Player.gameObject.SetActive(false);

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

        Game.Player.transform.position = savedPlayerLocation;
        Game.Player.gameObject.SetActive(true);
        SceneManager.sceneLoaded -= restorePlayerPositonAndGameObject;
    }
}
