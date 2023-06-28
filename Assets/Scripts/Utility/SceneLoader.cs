using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{

    private static int battleSceneBuildIndex = 1;
    private static int savedSceneBuildIndex;
    private static Vector2 savedPlayerLocation;
    private Player player;
    private Map map;

    public SceneLoader(Player player, Map map)
    {
        this.player = player;
        this.map = map;
    }

    public void loadBattleScene()
    {
        //Save current
        savedSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        savedPlayerLocation = map.grid.Center2D(player.currCell);

        // Player
        player.gameObject.SetActive(false);

        //Scene transition
        SceneManager.LoadScene(battleSceneBuildIndex);
    }

    public void reloadSavedScene()
    {
        SceneManager.sceneLoaded += restorePlayerPositonAndGameObject;
        SceneManager.LoadScene(savedSceneBuildIndex);
    }

    public void restorePlayerPositonAndGameObject(Scene scene, LoadSceneMode mode)
    {

        player.transform.position = savedPlayerLocation;
        player.gameObject.SetActive(true);
        SceneManager.sceneLoaded -= restorePlayerPositonAndGameObject;
    }
}
