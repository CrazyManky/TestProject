using _Project.Scripts.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameStateMachine.EntryPoint
{
    public class EntryPointGame 
    {
        private GameFSM _gameFsm;

        [Inject]
        public void Construct(GameFSM gameFsm)
        {
            _gameFsm = gameFsm;
        }

        public void Start()
        {
            _gameFsm.Enter<LoadingState>();
        }
    }
}