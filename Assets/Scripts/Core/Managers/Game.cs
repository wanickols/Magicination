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
    public GameState State { get; private set; }
    public static Map Map { get; private set; }
    public static Player Player { get; private set; }

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
            StartBattle();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndBattle();
        }
    }

    private void StartBattle()
    {
        SceneLoader.loadBattleScene();
        State = GameState.Battle;
    }

    private void EndBattle()
    {
        SceneLoader.reloadSavedScene();

        State = GameState.World;
    }
}
