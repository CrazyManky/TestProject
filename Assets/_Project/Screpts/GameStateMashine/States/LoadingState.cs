using _Project._Screpts.GameStateMashine;
using _Project._Screpts.Interfaces;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Screpts.ShopSystem;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Screpts.GameStateMashine.States
{
    public class LoadingState : IGameState
    {
        private GameFSM _gameFsm;
        private LoadingService _loadingService;
        private IAnalytics _analytics;
        private IConfigHandler _configHandler;
        private IAdsInitializer _adsInitializer;
        private IStoreInitialize _gameStoreInitialize;

        [Inject]
        public void Constructor(GameFSM gameFsm, LoadingService loadingService, IAnalytics analytics,
            IConfigHandler configHandler, IAdsInitializer adsInitializer, IStoreInitialize gameStoreInitialize)
        {
            _gameFsm = gameFsm;
            _loadingService = loadingService;
            _analytics = analytics;
            _configHandler = configHandler;
            _adsInitializer = adsInitializer;
            _gameStoreInitialize = gameStoreInitialize;
        }

        public async void EnterState()
        {
            await UnityServices.InitializeAsync();
            await _adsInitializer.InitializeAdsAsync();
            await _analytics.Initialize();
            _analytics.InvokeAppOpen();
            _gameStoreInitialize.InitializePurchasing();
            await _configHandler.DownloadAsync();
            await LoadNextSceneAsync();
        }

        private async UniTask LoadNextSceneAsync()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            await _loadingService.LoadFromFileAsync();

            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }

            _gameFsm.Enter<GamePlayState>();
        }


        public void ExitState()
        {
        }
    }
}