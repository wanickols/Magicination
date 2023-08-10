namespace Core
{
    public class StateManager
    {
        public GameState State { get; private set; } = GameState.World;
        public GameState previousState = GameState.World;

        //Game State Management
        public void changeState(GameState state)
        {
            previousState = State;
            State = state;
        }
        public void returnState() => State = previousState;


    }
}