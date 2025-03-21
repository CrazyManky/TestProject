﻿using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.Services;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.ShopSystem;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.GameStateMachine.States
{
    public class LoadingState : IGameState
    {
        private GameFSM _gameFsm;
        private LoadingService _loadingService;
        private IAnalytics _analytics;
        private IConfigHandler _configHandler;
        private IAdsInitializer _adsInitializer;
        private IStoreInitialize _gameStoreInitialize;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Constructor(GameFSM gameFsm, LoadingService loadingService, IAnalytics analytics,
            IConfigHandler configHandler, IAdsInitializer adsInitializer, IStoreInitialize gameStoreInitialize,
            SceneLoader sceneLoader)
        {
            _gameFsm = gameFsm;
            _loadingService = loadingService;
            _analytics = analytics;
            _configHandler = configHandler;
            _adsInitializer = adsInitializer;
            _gameStoreInitialize = gameStoreInitialize;
            _sceneLoader = sceneLoader;
        }

        public async void EnterState()
        {
            await UnityServices.InitializeAsync();
            await _analytics.Initialize();
            await UniTask.WhenAll(_adsInitializer.InitializeAdsAsync(), _configHandler.DownloadAsync(),
                _loadingService.LoadFromFileAsync(),
                _sceneLoader.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
            _gameFsm.Enter<GamePlayState>();
        }

        public void ExitState()
        {
            _analytics.NotifyAppOpen();
            _gameStoreInitialize.InitializePurchasing();
        }
    }
}