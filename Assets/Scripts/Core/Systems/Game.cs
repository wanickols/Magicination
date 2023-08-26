using UnityEngine;

namespace MGCNTN.Core
{
    public class Game : MonoBehaviour
    {

        //Seriazlied
        [SerializeField] private GameObject playerPrefab, uiPrefab;
        [SerializeField] private Vector2Int startingCell;
        [SerializeField] private Map startingMap;

        [Header("Managers")]
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] public EncounterManager encounterManager = new EncounterManager();

        /// Public
        public static Game manager { get; private set; }
        public Player player { get; private set; }
        public GameState State => stateManager.State; //Only used for nonmanager classes please

        //Managers
        public CutsceneManager cutsceneManager { get; private set; }
        public MapManager mapManager;


        ///Private
        //Managers
        public StateManager stateManager = new StateManager();
        private UI uiManager;
        private SceneLoader sceneLoader;


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
            initUI();
            initMap();
            initPlayer(); // map
            initCutscene(); // state map


            Consumable potion = Resources.Load<Consumable>("items/consumables/potion");
            Consumable revive = Resources.Load<Consumable>("items/consumables/revive");

            Party.bag.clear();
            Party.bag.Add(potion);
            Party.bag.Add(revive);

        }

        private void Start()
        {

            initSceneLoader(); // player, map 
            initEncounterManager(); // sceneLoader, map, stateManager
            initInput(); // player, UI, Map
            initDialogue(); //UI
            DontDestroyOnLoad(this);
        }

        //Init Functions
        private void initEncounterManager() => encounterManager.init(stateManager, sceneLoader, mapManager);
        private void Update() => inputHandler.CheckInput();
        private void initUI()
        {
            uiManager = Instantiate(uiPrefab, this.transform).GetComponent<UI>();
            uiManager.init(stateManager);
        }
        private void initMap() => mapManager = new MapManager(startingMap);
        private void initPlayer()
        {
            player = Instantiate(playerPrefab, mapManager.grid.Center2D(startingCell), Quaternion.identity).GetComponent<Player>();
            DontDestroyOnLoad(player);
        }
        private void initCutscene() => cutsceneManager = new CutsceneManager(stateManager);
        private void initSceneLoader() => sceneLoader = new SceneLoader();
        private void initInput() => inputHandler = new InputHandler(player, uiManager.mainMenu, mapManager, stateManager);
        private void initDialogue() => DialogueManager.instance.Init(inputHandler);
    }
}