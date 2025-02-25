using _Project._Screpts.Interfaces;
using _Project._Screpts.Services;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Services;
using _Project.Screpts.Services.LoadSystem;
using _Project.Scripts.GameStateMachine;
using _Project.Scripts.GameStateMachine.States;
using _Project.Scripts.Services.SaveSystem;
using Zenject;

namespace _Project.Screpts.GameStateMashine.States
{
    public class GameOverState : IGameState
    {
        private GameFSM _gameStateMachine;
        private PlayerObjectCollector _playerObjectCollector;
        private SaveDataHandler _saveDataHandler;
        private LoadingService _loadingService;
        private IShowReward _showReward;

        [Inject]
        public void Construct(GameFSM gameStateMachine, PlayerObjectCollector playerObjectCollector,
            SaveDataHandler saveDataHandler, LoadingService loadingService, IShowReward showReward)
        {
            _gameStateMachine = gameStateMachine;
            _playerObjectCollector = playerObjectCollector;
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _showReward = showReward;
        }

        public void EnterState()
        {
          //  _saveDataHandler.ClearSaveItems();
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