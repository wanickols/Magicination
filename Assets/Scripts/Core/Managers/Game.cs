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
    [SerializeField] private GameObject playerPrefab, uiPrefab, transitionPrefab;
    [SerializeField] private Vector2Int startingCell;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private SceneLoader sceneLoader;

    //Private
    private GameState previousState = GameState.World;
    private UI uiManager;
    private Player player;
    private MainMenu mainMenu;

    //Public
    public Map Map { get; private set; }

    //Awake and Start
    private void Awake()
    {
        //Singleton implementations (yes I know issues with these)
        if (manager != null && manager != this)
            Destroy(this);
        else
            manager = this;

        //Gamestate
        State = GameState.World;
        initMap();
        initUI();
        initPlayer(); //map
    }
    private void Start()
    {

        initSceneLoader(); // player, map 
        initInput(); // player, UI, Map
        initDialogue(); //UI
        initEvents(); //Dialogue, UI
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        inputHandler.CheckInput();
    }

    //Init Functions
    private void initMap()
    {
        Map = Instantiate(startingMap);
        DontDestroyOnLoad(Map);
    }
    private void initUI()
    {
        uiManager = Instantiate(uiPrefab, this.transform).GetComponent<UI>();
        initMenu();
    }
    private void initPlayer()
    {
        player = Instantiate(playerPrefab, Map.grid.Center2D(startingCell), Quaternion.identity).GetComponent<Player>();
        DontDestroyOnLoad(player);
    }
    private void initMenu() => mainMenu = GetComponentInChildren<MainMenu>();
    private void initSceneLoader() => sceneLoader = new SceneLoader(player, Map);
    private void initInput() => inputHandler = new InputHandler(player, mainMenu, Map);

    private void initDialogue() => DialogueManager.instance.Init(inputHandler);


    //Events
    private void initEvents()
    {
        //Dialogue
        DialogueManager.instance.openDialogue += openDialogue;
        DialogueManager.instance.closeDialogue += closeDialogue;

        //Menu
        mainMenu.openMenu += openMenu;
        mainMenu.closeMenu += closeMenu;

    }
    private void destroyEvents()
    {
        //Dialogue
        DialogueManager.instance.openDialogue -= openDialogue;
        DialogueManager.instance.closeDialogue -= closeDialogue;

        //Menu
        mainMenu.openMenu -= openMenu;
        mainMenu.closeMenu -= closeMenu;

    }

    //Event Listeners
    private void openDialogue(string name, Sprite sprite) => changeState(GameState.Dialogue);
    private void closeDialogue() => returnState();

    private void openMenu() => changeState(GameState.Menu);
    private void closeMenu() => returnState();

    //Game State Management
    private void changeState(GameState state)
    {
        previousState = State;
        State = state;
    }
    private void returnState() => State = previousState;

    //TODO: move this to scene loader, can use event
    private void StartBattle(EnemyPack pack)
    {

        previousState = State = GameState.Battle;
        Map.gameObject.SetActive(false);
        Battle.enemyPack = pack;

        sceneLoader.loadScene(SceneLoader.scene.battle, transitionPrefab);

    }

    private void EndBattle()
    {
        if (State == GameState.Battle)
        {
            Map.gameObject.SetActive(true);
            sceneLoader.loadScene(SceneLoader.savedScene);

            previousState = State = GameState.World;

        }
    }

    private void OnDestroy()
    {
        destroyEvents();
    }


}
