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

        public bool isTakingTurn => data.currentActor.turner.isTakingTurn;

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
            data.currentActor.turner.StartTurn();
            data.currentActor.turner.turnTime += CalculateTurnTime(data.currentActor.baseTurnSpeed);

            turnQueue.Enqueue(data.currentActor, data.currentActor.turner.turnTime);

            nextTurn?.Invoke();
        }

        public void DetermineTurnOrder(TurnBar turnBar)
        {
            if (data.nextSix.Count > 0)
                emptyNextSix();

            Actor nextActor = turnQueue.Dequeue();
            data.currentActor = nextActor;

            for (int i = 0; i < 6; i++)
            {
                while (nextActor.IsDead)
                    nextActor = turnQueue.Dequeue();

                data.nextSix.Add(nextActor);

                enqueue(nextActor);

                nextActor = turnQueue.Dequeue();
            }

            turnBar.SpawnPortraitSlots(data.nextSix);
        }


        public void enqueue(Actor actor)
        {
            actor.turner.turnTime += CalculateTurnTime(actor.baseTurnSpeed);
            turnQueue.Enqueue(actor, actor.turner.turnTime);
        }


        private void emptyNextSix()
        {
            for (int i = 0; i < turnQueue.Count; i++)
            {
                data.nextSix.Add(turnQueue.Dequeue());
            }

            foreach (Actor actor in data.nextSix)
            {
                enqueue(actor);
            }
            data.nextSix.Clear();
        }

        private float CalculateTurnTime(float speed)
        {
            //Debug.Log("Speed: " + speed);
            // Use the formula to calculate the turn time based on speed and other factors
            return baseTime * speed + UnityEngine.Random.Range(-randomFactor, randomFactor);
        }

    }
}