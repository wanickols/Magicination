using System;

namespace Core
{
    public class Player : Character
    {

        //Events
        public event Action<Transfer> TeleportPlayer;
        public event Action TriggerBattle;
        private int stepCount = 0;


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
            stepCount++;
            if (map.transfers.ContainsKey(currCell))
            {
                Transfer transfer = map.transfers[currCell];
                TeleportPlayer?.Invoke(transfer);
            }

            checkBattle();

        }

        private void checkBattle()
        {
            Region region = Game.manager.Map.region;
            if (region == null)
                return;

            int danger = (int)region.dangerLevel * 4;

            if (danger != 0 && stepCount > danger)
            {
                int chance = UnityEngine.Random.Range(0, 100);
                int enc = Party.getEncounterRate();

                if (chance < enc)
                {
                    TriggerBattle?.Invoke();
                    stepCount = 0;
                }
            }
        }

        public override void setCurrCell()
        {
            playerLocation.location = mover.currCell;
        }
    }
}