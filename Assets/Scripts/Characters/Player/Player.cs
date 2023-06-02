public class Player : Character
{

    private InputHandler InputHandler;

    protected override void Awake()
    {
        base.Awake();
        InputHandler = new InputHandler(this);
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
        InputHandler.CheckInput();
    }
}
