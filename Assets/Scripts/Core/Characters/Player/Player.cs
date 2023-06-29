namespace Core
{
    public class Player : Character
    {

        protected override void Awake()
        {
            base.Awake();
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

        }

        public void CheckCurrentCell(Map map)
        {
            if (map.Exits.ContainsKey(currCell))
            {
                Exit exit = map.Exits[currCell];
                exit.TeleportPlayer();
            }
        }

        public override void setCurrCell()
        {
            playerLocation.location = mover.currCell;
        }
    }
}