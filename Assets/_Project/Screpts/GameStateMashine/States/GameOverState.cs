using _Project._Screpts.GameStateMashine;
using _Project._Screpts.Interfaces;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Services;
using _Project.Screpts.Services.LoadSystem;
using Zenject;

namespace _Project.Screpts.GameStateMashine.States
{
    public class GameOverState : IGameState
    {
        private GameFSM _gameStateMachine;
        private PlayerObjectCollector _playerObjectCollector;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private IShowReward _showReward;

        [Inject]
        public void Construct(GameFSM gameStateMachine, PlayerObjectCollector playerObjectCollector,
            SaveService saveService, LoadingService loadingService, IShowReward showReward)
        {
            _gameStateMachine = gameStateMachine;
            _playerObjectCollector = playerObjectCollector;
            _saveService = saveService;
            _loadingService = loadingService;
            _showReward = showReward;
        }

        public void EnterState()
        {
            _saveService.ClearSaveItems();
            _loadingService.ClearLoadingItems();
            _playerObjectCollector.RemoveItems();
            _showReward.ResetCount();
            _gameStateMachine.Enter<GamePlayState>();
        }

        public void ExitState()
        {
        }
    }
}