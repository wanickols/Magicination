using System;
/// <summary>
/// This is a system designed to handle all of the turn related logic. 
/// </summary>
/// <remarks>
/// Initted in Battle Manager and works with battleUI and Battle manager
/// </remarks>
namespace MGCNTN.Battle
{
    public class TurnSystem
    {

        public Action nextTurn;

        private const float baseTime = 25f;
        private const float randomFactor = .2f;

        private BattleData data;

        public PriorityQueue<Actor, float> turnQueue { get; private set; } = new PriorityQueue<Actor, float>(); // Use a priority queue to store the actors and their turn times

        public bool isTakingTurn => data.currentActor.isTakingTurn;

        public TurnSystem(BattleUIManager battleUIManager, BattleData data)
        {
            this.data = data;
        }


        public void NextTurn(TurnBar turnBar)
        {
            data.currentActor = data.nextSix[0];
            data.nextSix.RemoveAt(0);

            Actor actor = turnQueue.Dequeue();

            int i = 0;
            while (actor.IsDead)
            {
                if (turnQueue.Count > 0)
                    actor = turnQueue.Dequeue();
                else
                {
                    enqueue(data.nextSix[i]);
                    i++;
                }
            }

            data.nextSix.Add(actor);
            turnBar.SpawnPortraitSlots(data.nextSix);
            data.currentActor.commander.StartTurn();
            enqueue(data.currentActor);

            nextTurn?.Invoke();
        }

        public void DetermineTurnOrder(TurnBar turnBar)
        {
            int count = data.nextSix.Count;

            if (count > 0)
                count = refreshTurnOrder();

            Actor nextActor;
            for (int i = 0; i < (6 - count); i++)
            {
                nextActor = turnQueue.Dequeue();

                while (nextActor.IsDead)
                    nextActor = turnQueue.Dequeue();

                data.nextSix.Add(nextActor);

                enqueue(nextActor);
            }

            data.currentActor = data.nextSix[0];
            turnBar.SpawnPortraitSlots(data.nextSix);
        }

        private int refreshTurnOrder()
        {

            for (int i = 0; i < data.nextSix.Count; i++)
            {
                if (data.nextSix[i].IsDead)
                {
                    data.nextSix.RemoveAt(i);
                    i--;
                }
            }

            return data.nextSix.Count;
        }

        public void enqueue(Actor actor)
        {
            actor.turnTime += CalculateTurnTime(actor.baseTurnSpeed);
            turnQueue.Enqueue(actor, actor.turnTime);
        }

        private float CalculateTurnTime(float speed)
        {
            //Debug.Log("Speed: " + speed);
            // Use the formula to calculate the turn time based on speed and other factors
            return baseTime * speed + UnityEngine.Random.Range(-randomFactor, randomFactor);
        }

    }
}