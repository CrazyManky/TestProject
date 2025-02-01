using _Project._Screpts.Interfaces;
using Zenject;

namespace _Project._Screpts.GameStateMashine
{
    public class GameFSM 
    {
        private IGameState _entreState;
        private IInstantiator _instantiator;
        
        [Inject]
        public GameFSM(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }


        public void Enter<TState>() where TState : IGameState
        {
            _entreState?.ExitState();
            _entreState = _instantiator.Instantiate<TState>();
            _entreState.EnterState();
        }
    }
}