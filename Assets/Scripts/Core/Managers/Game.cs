using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void EndBattle()
    {
        SceneManager.LoadScene(0);
    }

    private void StartBattle()
    {
        SceneManager.LoadScene(1);

    }
}
