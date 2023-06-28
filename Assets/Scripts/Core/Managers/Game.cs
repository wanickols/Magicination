using System.Collections;
using UnityEngine;

public enum GameState
{
    World,
    Cutscene,
    Dialogue,
    Battle,
    Menu
}

public class Game : MonoBehaviour
{
    public static GameState State { get; private set; }

    private static GameState previousState = GameState.World;

    private static MainMenu mainMenu;
    private static DialogueManager dialogueManager;


    public static Map Map { get; private set; }
    public static Player Player { get; private set; }

    public static void ToggleMenu()
    {
        if (mainMenu.IsAnimating)
            return;

        if (mainMenu.isOpen)
        {

            State = previousState;
            mainMenu.Close();

        }
        else
        {
            previousState = State;
            State = GameState.Menu;
            mainMenu.Open();
        }

    }
    public static void OpenDialogue() => State = GameState.Dialogue;
    public static void CloseDialogue() => State = previousState;

    [SerializeField] private Map startingMap;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject DialogueManagerPrefab;
    [SerializeField] private GameObject MainMenuPrefab;
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

        if (dialogueManager == null)
        {
            GameObject dialogue = Instantiate(DialogueManagerPrefab, this.transform);
            dialogueManager = dialogue.GetComponent<DialogueManager>();
        }

        if (mainMenu == null)
        {
            GameObject menu = Instantiate(MainMenuPrefab, this.transform);
            mainMenu = menu.GetComponentInChildren<MainMenu>();
        }


        State = GameState.World;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(Map);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

            StartCoroutine(Co_StartBattle());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndBattle();
        }
    }

    private IEnumerator Co_StartBattle()
    {
        Map.gameObject.SetActive(false);
        Battle.enemyPack = ResourceLoader.Load<EnemyPack>(ResourceLoader.TwoEyes);

        previousState = State = GameState.Battle;
        Instantiate(ResourceLoader.Load<GameObject>(ResourceLoader.BattleTransition), Player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        SceneLoader.loadBattleScene();

    }

    private void EndBattle()
    {
        if (State == GameState.Battle)
        {
            Map.gameObject.SetActive(true);
            SceneLoader.reloadSavedScene();

            previousState = State = GameState.World;

        }
    }
}
