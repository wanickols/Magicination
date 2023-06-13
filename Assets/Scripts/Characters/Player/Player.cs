public class Player : Character
{

    private InputHandler inputHandler;
    public InputHandler InputHandler => inputHandler;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = new InputHandler(this);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        inputHandler.CheckInput();
    }
}
