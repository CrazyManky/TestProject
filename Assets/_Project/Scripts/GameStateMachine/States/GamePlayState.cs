using _Project._Screpts.Interfaces;
using _Project._Screpts.Services;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.PauseSystem;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services;
using _Project.Screpts.Services.Factory;
using _Project.Screpts.Services.Inputs;
using _Project.Screpts.Services.Level;
using _Project.Scripts.GameStateMachine.States;
using _Project.Scripts.Services.MoveItems;
using Services;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameStateMashine.States
{
    public class GamePlayState : IGameState,IStateEnter,IStateExit, IFixedTickable
    {
        private MovementPlayer _movementPlayer;
        private InputHandler _inputHandler;
        private PauseService _pauseService;
        private LevelInitializer _levelInitializer;
        private SwitchingService _switchingService;
        private GameUIFactory _gameUIFactory;
        private CameraFollowFactory _cameraFollowFactory;
        private HandlerLose _handlerLose;
        private IAdvertisingShow _advertisingShow;

        [Inject]
        public GamePlayState(InputHandler inputHandler, CameraFollowFactory cameraFollowFactory,
            LevelInitializer levelInitializer, MovementPlayer movementPlayer, GameUIFactory gameUIFactory,
            SwitchingService switchingService, PauseService pauseService, HandlerLose handlerLose,
            IAdvertisingShow advertisingShow)
        {
            _inputHandler = inputHandler;
            _cameraFollowFactory = cameraFollowFactory;
            _levelInitializer = levelInitializer;
            _movementPlayer = movementPlayer;
            _gameUIFactory = gameUIFactory;
            _switchingService = switchingService;
            _pauseService = pauseService;
            _handlerLose = handlerLose;
            _advertisingShow = advertisingShow;
        }

        public void EnterState()
        {
            _handlerLose.Subscribe();
            InitGamePlay();
            _inputHandler.Initialize();
            _advertisingShow.Initialize();
            _advertisingShow.Show();
        }

        private void InitGamePlay()
        {
            var cameraFollowConteiner = new GameObject("CameraFollow");
            var UIConteiner = new GameObject("UI");
            var levelConteiner = new GameObject("Level");
            var gameUIInstance = _gameUIFactory.InstanceGUI(UIConteiner.transform);
            var cameraFollowInstance = _cameraFollowFactory.InstanceCameraFollow(cameraFollowConteiner.transform);
            _switchingService.SubscribeElements(cameraFollowInstance, _movementPlayer, gameUIInstance);
            _levelInitializer.InitializeLevel(cameraFollowInstance, gameUIInstance, _movementPlayer,
                levelConteiner.transform);
            _pauseService.SetUI(gameUIInstance);
        }

        public void FixedTick() => _movementPlayer.FixedTick();


        public void ExitState()
        {
            _handlerLose.Unsubscribe();
            _cameraFollowFactory.DestroyCameraFollow();
            _levelInitializer.DestroyObject();
            _gameUIFactory.DestroyGameUI();
            _switchingService.UnsubscribeElements();
            _advertisingShow.DisposeShow();
        }
    }
}