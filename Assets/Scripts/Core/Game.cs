using System.Linq;
using UnityEngine;

namespace Core
{
    public class Game : MonoBehaviour
    {

        //Instance
        public static Game manager { get; private set; }
        public GameState State { get; private set; }

        //Seriazlied
        [SerializeField] private Map startingMap;
        [SerializeField] private GameObject playerPrefab, uiPrefab, transitionPrefab, sceneLoaderPrefab;
        [SerializeField] private Vector2Int startingCell;

        [SerializeField] private InputHandler inputHandler;


        //Private
        private GameState previousState = GameState.World;
        private UI uiManager;
        private Player player;
        private SceneLoader sceneLoader;
        private MainMenu mainMenu;

        private int battleChance = 50; //out of 100, chance to trigger a battle when character steps hit threshhold. 

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
        private void initSceneLoader()
        {
            sceneLoader = Instantiate(sceneLoaderPrefab, this.transform).GetComponent<SceneLoader>();
            sceneLoader.init(player);
        }
        private void initInput() => inputHandler = new InputHandler(player, mainMenu);
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

            player.TeleportPlayer += LoadMap;
            player.TriggerBattle += StartBattle;

        }

        private void destroyEvents()
        {
            //Dialogue
            DialogueManager.instance.openDialogue -= openDialogue;
            DialogueManager.instance.closeDialogue -= closeDialogue;

            //Menu
            mainMenu.openMenu -= openMenu;
            mainMenu.closeMenu -= closeMenu;

            //Map
            player.TeleportPlayer -= LoadMap;
            player.TriggerBattle -= StartBattle;

        }

        //Event Listeners
        private void openDialogue(string name, Sprite sprite) => changeState(GameState.Dialogue);
        private void closeDialogue() => returnState();

        private void openMenu() => changeState(GameState.Menu);
        private void closeMenu() => returnState();

        private void LoadMap(Transfer transfer)
        {
            Map oldMap = Map;

            Map = Instantiate(transfer.NewMap);

            Destroy(oldMap.gameObject);



            Transfer[] transfers = FindObjectsOfType<Transfer>();

            Transfer _transfer = transfers.Where(transfer => transfer.Id == transfer.DestinationId).FirstOrDefault();

            player.transform.position = Map.grid.Center2D(Map.grid.GetCell2D(_transfer.gameObject));
        }

        //Game State Management
        private void changeState(GameState state)
        {
            previousState = State;
            State = state;
        }
        private void returnState() => State = previousState;

        //TODO: move this to scene loader, can use event
        private void StartBattle()
        {

            previousState = State = GameState.Battle;
            Battle.Battle.currentRegion = Map.region;

            //transitionPrefab
            sceneLoader.loadScene(SceneLoader.scene.battle);
            Map.gameObject.SetActive(false);

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
}