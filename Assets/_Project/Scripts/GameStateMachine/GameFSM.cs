using _Project.Scripts.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameStateMachine
{
    public class GameFSM
    {
        private IStateExit _activeState;
        private IInstantiator _instantiator;

        [Inject]
        public GameFSM(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }


        public void Enter<TState>() where TState : IGameState
        {
            _activeState?.ExitState();
            var state = _instantiator.Instantiate<TState>();
            state.EnterState();
            _activeState = state;
        }
    }
}