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

            if (map.Triggers.ContainsKey(currCell))
            {
                ITriggerByTouch trigger = map.Triggers[currCell];
                trigger.Trigger();
                return;
            }


            Region region = Game.manager.Map.region;
            if (region != null)
                region.tryRandomEncounter();

        }



        public override void setCurrCell()
        {
            playerLocation.location = mover.currCell;
        }
    }
}