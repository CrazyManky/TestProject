using _Project._Screpts.GameStateMashine;
using _Project._Screpts.Interfaces;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using Cysharp.Threading.Tasks;
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
        private IAdvertisingShow _advertisingShow;

        [Inject]
        public void Constructor(GameFSM gameFsm, LoadingService loadingService, IAnalytics analytics,
            IConfigHandler configHandler, IAdsInitializer adsInitializer, IAdvertisingShow advertisingShow)
        {
            _gameFsm = gameFsm;
            _loadingService = loadingService;
            _analytics = analytics;
            _configHandler = configHandler;
            _adsInitializer = adsInitializer;
            _advertisingShow = advertisingShow;
        }

        public async void EnterState()
        {
            await _adsInitializer.InitializeAdsAsync();
            await _analytics.Initialize();
            _analytics.InvokeAppOpen();
            await _configHandler.DownloadAsync();
            _advertisingShow.Show();
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