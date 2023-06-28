using UnityEngine;


//Starts the game
public class StartGame : MonoBehaviour

{

    [SerializeField] private GameObject GameManager;
    [SerializeField] private Game game;

    private void Awake()
    {
        Start();
    }


    //Starts the game
    private void Start()
    {
        game = FindAnyObjectByType<Game>();
        if (game == null)
        {
            GameObject gameObject = Instantiate(GameManager);
            game = gameObject.GetComponent<Game>();
        }
        DontDestroyOnLoad(game);
    }

}
