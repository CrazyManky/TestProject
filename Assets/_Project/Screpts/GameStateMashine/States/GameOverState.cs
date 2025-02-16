using _Project._Screpts.GameStateMashine;
using _Project._Screpts.Interfaces;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services;
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

        [Inject]
        public void Construct(GameFSM gameStateMachine, PlayerObjectCollector playerObjectCollector,
            SaveService saveService, LoadingService loadingService)
        {
            _gameStateMachine = gameStateMachine;
            _playerObjectCollector = playerObjectCollector;
            _saveService = saveService;
            _loadingService = loadingService;
        }

        public void EnterState()
        {
            _saveService.ClearSaveItems();
            _loadingService.ClearLoadingItems();
            _playerObjectCollector.RemoveItems();
            _gameStateMachine.Enter<GamePlayState>();
        }

        public void ExitState()
        {
        }
    }
}