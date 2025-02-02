using _Project._Screpts.GameStateMashine;
using _Project.Screpts.GameStateMashine.States;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameStateMashine.EntryPoint
{
    public class EntryPointGame : MonoBehaviour
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