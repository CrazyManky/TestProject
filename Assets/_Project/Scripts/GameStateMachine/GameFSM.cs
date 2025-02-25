using _Project._Screpts.Interfaces;
using _Project.Scripts.GameStateMachine.States;
using Zenject;

namespace _Project.Scripts.GameStateMachine
{
    public class GameFSM 
    {
        private IGameState _activeState;
        private IInstantiator _instantiator;
        
        [Inject]
        public GameFSM(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }


        public void Enter<TState>() where TState : IGameState
        {
            _activeState?.ExitState();
            _activeState = _instantiator.Instantiate<TState>();
            _activeState.EnterState();
        }
    }
}