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

    //Instance
    public static Game manager { get; private set; }
    public GameState State { get; private set; }

    //Seriazlied
    [SerializeField] private Map startingMap;
    [SerializeField] private GameObject playerPrefab, DialogueManagerPrefab, MainMenuPrefab;
    [SerializeField] private Vector2Int startingCell;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private SceneLoader sceneLoader;

    //Private
    private GameState previousState = GameState.World;
    private DialogueManager dialogueManager;
    public Map Map { get; private set; }
    private Player player;

    //Init Functions
    private void initMap()
    {
        if (Map == null)
        {
            Map = Instantiate(startingMap);
        }
        DontDestroyOnLoad(Map);
    }

    private void initPlayer()
    {
        if (player == null)
        {
            GameObject gameObject = Instantiate(playerPrefab, Map.grid.Center2D(startingCell), Quaternion.identity);
            player = gameObject.GetComponent<Player>();
        }
        DontDestroyOnLoad(player);
    }



    private void initMenu()
    {
        if (mainMenu == null)
        {
            GameObject menu = Instantiate(MainMenuPrefab, this.transform);
            mainMenu = menu.GetComponentInChildren<MainMenu>();
        }
    }
    private void initSceneLoader()
    {
        sceneLoader = new SceneLoader(player, Map);
    }
    private void initInput()
    {
        if (player == null)
            initPlayer(); //Should never run

        if (mainMenu == null)
            initMenu();

        inputHandler = new InputHandler(player, mainMenu, Map);
    }
    private void initDialogue()
    {
        if (dialogueManager == null)
        {
            GameObject dialogue = Instantiate(DialogueManagerPrefab, this.transform);
            dialogueManager = dialogue.GetComponent<DialogueManager>();
            dialogueManager.Init(inputHandler);
        }
    }



    //Awake
    private void Awake()
    {

        //Singleton implementations (yes I know issues with these)
        if (manager != null && manager != this)
            Destroy(this);
        else
            manager = this;


        initMap();
        initMenu();
        initPlayer(); //map
        initSceneLoader(); // player, map 
        initInput(); // player, menu, Map
        initDialogue(); //input



        //Gamestate
        State = GameState.World;
        DontDestroyOnLoad(this);

    }

    //Game State Management
    public void changeState(GameState state)
    {
        previousState = State;
        State = state;
    }
    public void returnState() => State = previousState;


    //Testing
    private void Update()
    {

        inputHandler.CheckInput();

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
        Instantiate(ResourceLoader.Load<GameObject>(ResourceLoader.BattleTransition), player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        sceneLoader.loadBattleScene();

    }

    private void EndBattle()
    {
        if (State == GameState.Battle)
        {
            Map.gameObject.SetActive(true);
            sceneLoader.reloadSavedScene();

            previousState = State = GameState.World;

        }
    }


}
