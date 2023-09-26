namespace MGCNTN.Core
{
    public class Player : Character
    {
        public void CheckCurrentCell(Map map)
        {

            if (map.Triggers.ContainsKey(currCell))
            {
                ITriggerByTouch trigger = map.Triggers[currCell];
                trigger.Trigger();
                return;
            }


            if (map.region != null)
                map.region.tryRandomEncounter();

        }


        public override void setCurrCell()
        {
            playerLocation.location = mover.currCell;
        }
    }
}