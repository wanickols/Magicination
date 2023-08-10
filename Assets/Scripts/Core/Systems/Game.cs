using UnityEngine;

namespace Core
{
    public class Game : MonoBehaviour
    {

        /// Public
        public static Game manager { get; private set; }

        public GameState State => stateManager.State; //Only used for nonmanager classes please

        ///Private
        //Seriazlied

        [SerializeField] private GameObject playerPrefab, uiPrefab;
        [SerializeField] private Vector2Int startingCell;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Map startingMap;
        public MapManager mapManager;
        [SerializeField] private EncounterManager encounterManager;


        private UI uiManager;
        public Player player { get; private set; }
        private SceneLoader sceneLoader;

        private MainMenu mainMenu;

        //Managers
        public CutsceneManager cutsceneManager { get; private set; }



        private StateManager stateManager;


        ///Private Functions

        //Awake and Start
        private void Awake()
        {
            //Singleton implementations (yes I know issues with these)
            if (manager != null && manager != this)
                Destroy(this);
            else
                manager = this;

            //Gamestate
            initState();
            initUI();
            initMap();
            initPlayer(); // map
            initCutscene();

        }


        private void Start()
        {

            initSceneLoader(); // player, map 
            initEncounterManager(); // sceneLoader, map, stateManager
            initInput(); // player, UI, Map
            initDialogue(); //UI
            initEvents(); //Dialogue, UI
            DontDestroyOnLoad(this);
        }

        private void initEncounterManager()
        {
            encounterManager = new EncounterManager(stateManager, mapManager.map, sceneLoader);
        }

        private void Update()
        {

            inputHandler.CheckInput();
        }

        //Init Functions
        private void initState()
        {
            stateManager = new StateManager();
        }
        private void initUI()
        {
            uiManager = Instantiate(uiPrefab, this.transform).GetComponent<UI>();
            initMenu();
        }
        private void initPlayer()
        {
            player = Instantiate(playerPrefab, mapManager.grid.Center2D(startingCell), Quaternion.identity).GetComponent<Player>();
            DontDestroyOnLoad(player);
        }

        private void initMap()
        {
            mapManager = new MapManager(encounterManager, startingMap);
        }

        private void initMenu() => mainMenu = GetComponentInChildren<MainMenu>();
        private void initCutscene()
        {
            cutsceneManager = new CutsceneManager(stateManager, mapManager.map);
        }
        private void initSceneLoader()
        {
            sceneLoader = new SceneLoader(player);
        }
        private void initInput() => inputHandler = new InputHandler(player, mainMenu, mapManager.map, stateManager);
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
        private void openDialogue(string name, Sprite sprite) => stateManager.changeState(GameState.Dialogue);
        private void closeDialogue() => stateManager.returnState();

        private void openMenu() => stateManager.changeState(GameState.Menu);
        private void closeMenu() => stateManager.returnState();




        private void OnDestroy()
        {
            destroyEvents();
        }


    }
}