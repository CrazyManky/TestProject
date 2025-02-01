using _Project._Screpts.GameItems.Enemy.Conteiner;
using _Project._Screpts.GameItems.GameLevels;
using _Project._Screpts.GameItems.PlayerObjects;
using _Project._Screpts.GameStateMashine.States;
using _Project._Screpts.LoadSystem;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.Level;
using _Project._Screpts.Services.MoveItems;
using _Project._Screpts.Services.PauseSystem;
using _Project._Screpts.UI;
using Services;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.GameStateMashine.EntryPoint
{
    public class ProjectContext : MonoInstaller
    {
        [SerializeField] private EnemyConteiner _enemyConteiner;
        [SerializeField] private ConteinerLevels _conteinerLevelsPrefab;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private PlayerObjectConteiner _playerObjectConteiner;
        [SerializeField] private GameUI _gameUI;

        private GameFSM _gameFSM;
        private EntryPoint _entryPoint;

        public override void InstallBindings()
        {
            RegisterStates(Container);
            RegisterServices(Container);
            RegisterFactories(Container);
            RegisterConteiners(Container);
            RegisterTickItems(Container);
            RegisterFixedTickItems(Container);
            RegisterGameObjects(Container);
            CreateGameFsm(Container);
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _entryPoint = new EntryPoint(_gameFSM);
            _entryPoint.Start();
        }


        private void CreateGameFsm(DiContainer container)
        {
            _gameFSM = new GameFSM(container);
            container.Bind<GameFSM>().FromInstance(_gameFSM);
        }

        private void RegisterStates(DiContainer container)
        {
            container.Bind<LoadingState>().AsTransient();
            container.Bind<GamePlayState>().AsTransient();
            container.Bind<GameOverState>().AsTransient();
        }

        private void RegisterTickItems(DiContainer container)
        {
            container.Bind<ITickable>().To<InputHandler>().FromResolve();
        }

        private void RegisterFixedTickItems(DiContainer container)
        {
            container.Bind<IFixedTickable>().To<GamePlayState>().FromResolve();
        }

        private void RegisterServices(DiContainer container)
        {
            container.Bind<IInputMovement>().To<InputMovement>().AsSingle();
            container.Bind<IInputGamePlayActionHandler>().To<GamePlayInput>().AsSingle();
            container.Bind<LevelInitializer>().AsSingle();
            container.Bind<InputHandler>().AsSingle();
            container.Bind<SaveService>().AsSingle();
            container.Bind<LoadingService>().AsSingle();
            container.Bind<PauseService>().AsSingle();
            container.Bind<PlayerObjectCollector>().AsSingle();
            container.Bind<MovePlayerItems>().AsSingle();
            container.Bind<SwitchObjectService>().AsSingle();
            container.Bind<HandlerLose>().AsSingle();
            container.Bind<LevelWinHandle>().AsSingle();
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
            container.Bind<PlayerObjectConteiner>().FromInstance(_playerObjectConteiner).AsSingle();
            container.Bind<ConteinerLevels>().FromInstance(_conteinerLevelsPrefab).AsSingle();
            container.Bind<EnemyConteiner>().FromInstance(_enemyConteiner).AsSingle();
        }
    }
}