using System;

namespace Core
{
    public class Player : Character
    {

        //Events
        public event Action<Transfer> TeleportPlayer;



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

            if (map.transfers.ContainsKey(currCell))
            {
                Transfer transfer = map.transfers[currCell];
                TeleportPlayer?.Invoke(transfer);
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