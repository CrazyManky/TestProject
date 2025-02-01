using _Project._Screpts.GameStateMashine;
using _Project.Screpts.GameStateMashine.States;

namespace _Project.Screpts.GameStateMashine.EntryPoint
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