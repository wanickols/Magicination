using System.Collections;
using UnityEngine;

public enum GameState
{
    World,
    Cutscene,
    Battle,
    Menu
}

public class Game : MonoBehaviour
{
    public static GameState State { get; private set; }
    public static Map Map { get; private set; }
    public static Player Player { get; private set; }

    public static void OpenMenu() => State = GameState.Menu;
    public static void CloseMenu() => State = GameState.World;

    [SerializeField] private Map startingMap;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector2Int startingCell;

    private void Awake()
    {
        if (Map == null)
        {
            Map = Instantiate(startingMap);
        }

        if (Player == null)
        {
            GameObject gameObject = Instantiate(playerPrefab, startingCell.Center2D(), Quaternion.identity);
            Player = gameObject.GetComponent<Player>();
        }

        State = GameState.World;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Battle.enemyPack = ResourceLoader.Load<EnemyPack>(ResourceLoader.TwoEyes);
            StartCoroutine(Co_StartBattle());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndBattle();
        }
    }

    private IEnumerator Co_StartBattle()
    {
        State = GameState.Battle;
        Instantiate(ResourceLoader.Load<GameObject>(ResourceLoader.BattleTransition), Player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        SceneLoader.loadBattleScene();

    }

    private void EndBattle()
    {
        SceneLoader.reloadSavedScene();

        State = GameState.World;
    }
}
