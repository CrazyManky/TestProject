using _Project._Screpts.GameStateMashine.States;
using Zenject;

namespace _Project._Screpts.GameStateMashine.EntryPoint
{
    public class EntryPoint
    {
        private GameFSM _gameFsm;

        public EntryPoint(GameFSM gameFsm)
        {
            _gameFsm = gameFsm;
        }

        public void Start()
        {
            _gameFsm.Enter<LoadingState>();
        }
    }
}