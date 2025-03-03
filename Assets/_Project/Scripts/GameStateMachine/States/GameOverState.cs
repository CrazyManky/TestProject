using _Project.Screpts.AdvertisingServices;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.GameStateMachine.States
{
    public class GameOverState : IGameState
    {
        private GameFSM _gameStateMachine;
        private PlayerObjectCollector _playerObjectCollector;
        private IShowReward _showReward;

        [Inject]
        public void Construct(GameFSM gameStateMachine, PlayerObjectCollector playerObjectCollector, IShowReward showReward)
        {
            _gameStateMachine = gameStateMachine;
            _playerObjectCollector = playerObjectCollector;
            _showReward = showReward;
        }

        public void EnterState()
        {
            _playerObjectCollector.RemoveItems();
            _showReward.ResetCount();
            _gameStateMachine.Enter<GamePlayState>();
        }

        public void ExitState()
        {
        }
    }
}