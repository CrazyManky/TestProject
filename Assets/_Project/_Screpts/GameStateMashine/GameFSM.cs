using _Project._Screpts.Interfaces;
using Zenject;

namespace _Project._Screpts.GameStateMashine
{
    public class GameFSM 
    {
        private IGameState _entreState;
        private DiContainer _container;

        public GameFSM(DiContainer container)
        {
            _container = container;
        }


        public void Enter<TState>() where TState : IGameState
        {
            _entreState?.ExitState();
            _entreState = _container.Instantiate<TState>();
            _entreState.EnterState();
        }
    }
}