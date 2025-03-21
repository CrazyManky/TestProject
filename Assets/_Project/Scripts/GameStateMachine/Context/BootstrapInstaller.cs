using _Project.Screpts.Services.Conteiner;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameItems.EnemyComponents;
using _Project.Scripts.GameItems.GameLevel;
using _Project.Scripts.GameItems.PlayerItems;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.GameStateMachine.EntryPoint;
using _Project.Scripts.GameStateMachine.States;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.Inputs;
using _Project.Scripts.Services.Level;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.MoveItems;
using _Project.Scripts.Services.PauseSystem;
using _Project.Scripts.Services.SaveSystem;
using _Project.Scripts.ShopSystem;
using _Project.Scripts.UI;
using _Project.Scripts.UI.SaveAndLoadUI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameStateMachine.Context
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameItemsConteiner<BaseEnemy> _enemies;
        [SerializeField] private GameLevel gameLevelPrefab;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private GameItemsConteiner<PlayerItem> _playerObjects;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private GameOverUI gameOverUI;
        [SerializeField] private AudioService _audioServicePrefab;

        private GameFSM _gameFSM;
        private EntryPointGame _entryPoint;

        public override void InstallBindings()
        {
            RegisterStates(Container);
            RegisterFactories(Container);
            RegisterServices(Container);
            RegisterContainers(Container);
            RegisterGameObjects(Container);
            RegisterModel(Container);
            CreateGameFsm(Container);
            RegisterEntryPoint(Container);
        }

        private void RegisterEntryPoint(DiContainer container)
        {
            container.Bind<IInitializable>().To<EntryPointGame>().AsSingle();
            _entryPoint = new EntryPointGame();
            container.Inject(_entryPoint);
        }

        private void CreateGameFsm(DiContainer container)
        {
            _gameFSM = new GameFSM(container);
            container.Bind<GameFSM>().FromInstance(_gameFSM);
        }

        private void RegisterStates(DiContainer container)
        {
            container.Bind<LoadingState>().AsSingle();
            container.BindInterfacesAndSelfTo<GamePlayState>().AsSingle();
            container.Bind<GameOverState>().AsSingle();
        }


        private void RegisterServices(DiContainer container)
        {
            container.Bind<LevelInitializer>().AsSingle();
            container.BindInterfacesAndSelfTo<LoadingService>().AsSingle();
            container.Bind<HandlerLose>().AsSingle();
            container.Bind<LevelWinHandle>().AsSingle();
            container.Bind<GameObjectDestroyer>().AsSingle();
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
            container.Bind<GameOverUI>().FromInstance(gameOverUI).AsSingle();
            container.Bind<AudioService>().FromInstance(_audioServicePrefab).AsSingle();
        }

        private void RegisterContainers(DiContainer container)
        {
            container.Bind<GameItemsConteiner<PlayerItem>>().FromInstance(_playerObjects).AsSingle();
            container.Bind<GameLevel>().FromInstance(gameLevelPrefab).AsSingle();
            container.Bind<GameItemsConteiner<BaseEnemy>>().FromInstance(_enemies).AsSingle();
        }
    }
}