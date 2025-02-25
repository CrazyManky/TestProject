namespace _Project.Scripts.GameStateMachine.States
{
    public interface IGameState : IStateEnter,IStateExit
    {
    }


    public interface IStateEnter
    {
        public void EnterState();
    }

    public interface IStateExit
    {
        public void ExitState();
    }
}