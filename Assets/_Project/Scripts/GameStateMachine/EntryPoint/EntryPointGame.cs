using _Project.Scripts.GameStateMachine.States;
using Zenject;

namespace _Project.Scripts.GameStateMachine.EntryPoint
{
    public class EntryPointGame : IInitializable
    {
        private GameFSM _gameFsm;

        [Inject]
        public void Construct(GameFSM gameFsm) => _gameFsm = gameFsm;

        public void Initialize() => _gameFsm.Enter<LoadingState>();
    }
}