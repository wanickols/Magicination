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



    //Public
    public GameState State { get; private set; }
    private Map Map;
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
            GameObject gameObject = Instantiate(playerPrefab, startingCell.Center2D(), Quaternion.identity);
            player = gameObject.GetComponent<Player>();
        }
        DontDestroyOnLoad(player);
    }

    private void initDialogue()
    {
        if (dialogueManager == null)
        {
            GameObject dialogue = Instantiate(DialogueManagerPrefab, this.transform);
            dialogueManager = dialogue.GetComponent<DialogueManager>();
        }
    }

    private void initMenu()
    {
        if (mainMenu == null)
        {
            GameObject menu = Instantiate(MainMenuPrefab, this.transform);
            mainMenu = menu.GetComponentInChildren<MainMenu>();
        }
    }

    private void initInput()
    {
        if (player != null)
            initPlayer(); //Should never run

        inputHandler = new InputHandler(player);
    }

    private void initSceneLoader()
    {
        sceneLoader = new SceneLoader();
        SceneLoader.player = player;
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
        initPlayer();
        initDialogue();
        initMenu();

        //Input
        initInput();

        //Scene
        initSceneLoader();
        //Battle

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


    //--------Interaction between systems-----------//

    //------------Menu------------//
    public void ToggleMenu() => mainMenu.toggle();

    //------------Map------------//
    //Grid
    public float CellSize => Map.cellsize;

    //Cells
    public Vector2Int MapGetCell2D(GameObject gameObject)
    {
        return Map.GetCell2D(gameObject);
    }
    public Vector2 MapGetCellCenter2D(Vector2Int cell)
    {
        return Map.GetCellCenter2D(cell);
    }

    //Occupied Cells
    public void MapAddCell(Vector2Int cell, MonoBehaviour mono) => Map.addCell(cell, mono);
    public void MapRemoveCell(Vector2Int cell) => Map.removeCell(cell);
    public bool MapContainsKey(Vector2Int cell) => Map.containsKey(cell);

    public IInteractable MapIsInteractable(Vector2Int cell) => Map.isInteractable(cell);


    //Directions
    public Vector2 GetCellCenterWorld(Vector3Int threeDimenCell) => Map.GetCellCenterWorld(threeDimenCell);

    //------------Input------------//
    public bool ContinueDialogueCheck() => inputHandler.ContinueDialogueCheck();

    private IEnumerator Co_StartBattle()
    {
        Map.gameObject.SetActive(false);
        Battle.enemyPack = ResourceLoader.Load<EnemyPack>(ResourceLoader.TwoEyes);

        previousState = State = GameState.Battle;
        Instantiate(ResourceLoader.Load<GameObject>(ResourceLoader.BattleTransition), player.transform.position, Quaternion.identity);
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


    //Test Functions
    public MonoBehaviour getOccupuiedCell(Vector2Int cell) => Map.getOccupuiedCell(cell);
}
