public class Enemy : Actor
{
    protected override void Start()
    {
        base.Start();
        float offset = -2f;
        targetPosition.x = startingPosition.x + offset;
    }

    //Will Eventually be enemy actor script
    public override void StartTurn()
    {
        //TODO
    }
}
