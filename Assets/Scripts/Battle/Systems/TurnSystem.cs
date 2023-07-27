using System;
/// <summary>
/// This is a system designed to handle all of the turn related logic. 
/// </summary>
/// <remarks>
/// Initted in Battle Manager and works with battleUI and Battle manager
/// </remarks>
namespace Battle
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
            data.nextSix.Add(turnQueue.Dequeue());
            turnBar.SpawnPortraitSlots(data.nextSix);
            data.currentActor.StartTurn();
            data.currentActor.turnTime += CalculateTurnTime(data.currentActor.baseTurnSpeed);

            turnQueue.Enqueue(data.currentActor, data.currentActor.turnTime);

            nextTurn?.Invoke();
        }

        public void DetermineTurnOrder(TurnBar turnBar)
        {
            int loopCount = turnQueue.Count;
            if (loopCount < 6)
                loopCount = 6;

            Actor nextActor = turnQueue.Dequeue();
            data.currentActor = nextActor;

            for (int i = 0; i < loopCount; i++)
            {
                data.nextSix.Add(nextActor);

                enqueue(nextActor);

                nextActor = turnQueue.Dequeue();
            }

            turnBar.SpawnPortraitSlots(data.nextSix);
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