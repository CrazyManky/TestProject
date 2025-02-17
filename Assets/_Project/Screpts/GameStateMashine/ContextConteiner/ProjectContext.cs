using _Project._Screpts.GameStateMashine;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.Level;
using _Project._Screpts.Services.PauseSystem;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Analytics_Service;
using _Project.Screpts.GameItems.EnemyComponents;
using _Project.Screpts.GameItems.GameLevel;
using _Project.Screpts.GameItems.PlayerObjects;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.GameStateMashine.EntryPoint;
using _Project.Screpts.GameStateMashine.States;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services;
using _Project.Screpts.Services.Conteiner;
using _Project.Screpts.Services.Factory;
using _Project.Screpts.Services.Inputs;
using _Project.Screpts.Services.Level;
using _Project.Screpts.Services.LoadSystem;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Screpts.Services.MoveItems;
using _Project.Screpts.UI;
using _Project.Screpts.UI.SaveAndLoadUI;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameStateMashine.ContextConteiner
{
    public class ProjectContext : MonoInstaller
    {
        [SerializeField] private GameItemsConteiner<EnemyObject> _enemyConteiner;
        [SerializeField] private GameLevel gameLevelPrefab;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private GameItemsConteiner<MoveObject> _playerObjectConteiner;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private EntryPointGame _entryPoint;

        private GameFSM _gameFSM;

        public override void InstallBindings()
        {
            RegisterEntryPoint(Container);
            RegisterStates(Container);
            RegisterServices(Container);
            RegisterFactories(Container);
            RegisterConteiners(Container);
            RegisterGameObjects(Container);
            RegisterAnalyticService(Container);
            RegisterModel(Container);
            CreateGameFsm(Container);
            DontDestroyOnLoad(this);
        }


        private void RegisterEntryPoint(DiContainer container)
        {
            container.Bind<EntryPointGame>().FromInstance(_entryPoint).AsSingle();
        }

        private void CreateGameFsm(DiContainer container)
        {
            _gameFSM = new GameFSM(container);
            container.Bind<GameFSM>().FromInstance(_gameFSM);
        }

        private void RegisterStates(DiContainer container)
        {
            container.Bind<LoadingState>().AsTransient();
            container.BindInterfacesAndSelfTo<GamePlayState>().AsTransient();
            container.Bind<GameOverState>().AsTransient();
        }

        private void RegisterAnalyticService(DiContainer container)
        {
            container.Bind<IAnalytics>().To<FirebaseWrapper>().AsSingle();
        }

        private void RegisterServices(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            container.BindInterfacesAndSelfTo<UnityAdsInitializer>().AsSingle();
            container.Bind<LevelInitializer>().AsSingle();
            container.Bind<SaveService>().AsSingle();
            container.Bind<LoadingService>().AsSingle();
            container.Bind<PauseService>().AsSingle();
            container.Bind<PlayerObjectCollector>().AsSingle();
            container.Bind<MovementPlayerObjects>().AsSingle();
            container.Bind<SwitchingService>().AsSingle();
            container.Bind<HandlerLose>().AsSingle();
            container.Bind<LevelWinHandle>().AsSingle();
            container.Bind<IConfigHandler>().To<ConfigHandler>().AsSingle();
            container.Bind<IShowReward>().To<AdvertisingShowReward>().AsCached();
            container.Bind<IAdvertisingShow>().To<AdvertisingShow>().AsSingle();
        }

        private void RegisterModel(DiContainer container)
        {
            container.Bind<SaveAndLoadModel>().AsSingle();
        }


        private void RegisterFactories(DiContainer container)
        {
            container.Bind<FactoryPlayerObjects>().AsSingle();
            container.Bind<EnemyFactory>().AsTransient();
            container.Bind<CameraFollowFactory>().AsTransient();
            container.Bind<GameUIFactory>().AsTransient();
        }

        private void RegisterGameObjects(DiContainer container)
        {
            container.Bind<CameraFollow>().FromInstance(cameraFollow).AsSingle();
            container.Bind<GameUI>().FromInstance(_gameUI).AsSingle();
        }

        private void RegisterConteiners(DiContainer container)
        {
            container.Bind<GameItemsConteiner<MoveObject>>().FromInstance(_playerObjectConteiner).AsSingle();
            container.Bind<GameLevel>().FromInstance(gameLevelPrefab).AsSingle();
            container.Bind<GameItemsConteiner<EnemyObject>>().FromInstance(_enemyConteiner).AsSingle();
        }
    }
}