using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.Level;
using _Project.Scripts.Services.MoveItems;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameStateMachine.States
{
    public class GamePlayState : IGameState, IFixedTickable
    {
        private MovementPlayer _movementPlayer;
        private LevelInitializer _levelInitializer;
        private SwitchingService _switchingService;
        private GameUIFactory _gameUIFactory;
        private CameraFollowFactory _cameraFollowFactory;
        private HandlerLose _handlerLose;
        private IAdvertisingShow _advertisingShow;
        private GameObjectDestroyer _destroyer;

        [Inject]
        public GamePlayState(CameraFollowFactory cameraFollowFactory,
            LevelInitializer levelInitializer, MovementPlayer movementPlayer, GameUIFactory gameUIFactory,
            SwitchingService switchingService, HandlerLose handlerLose,
            IAdvertisingShow advertisingShow, GameObjectDestroyer destroyer)
        {
            _cameraFollowFactory = cameraFollowFactory;
            _levelInitializer = levelInitializer;
            _movementPlayer = movementPlayer;
            _gameUIFactory = gameUIFactory;
            _switchingService = switchingService;
            _handlerLose = handlerLose;
            _advertisingShow = advertisingShow;
            _destroyer = destroyer;
        }

        public void EnterState()
        {
            _handlerLose.Subscribe();
            InitGamePlay();
            _advertisingShow.Initialize();
            _advertisingShow.Show();
        }

        private void InitGamePlay()
        {
            var gameObject = new GameObject("level");
            _destroyer.AddItem(gameObject);
            var gameUIInstance = _gameUIFactory.InstanceGUI(gameObject.transform);
            var cameraFollowInstance = _cameraFollowFactory.InstanceCameraFollow(gameObject.transform);
            _switchingService.SubscribeElements(cameraFollowInstance, _movementPlayer, gameUIInstance);
            _levelInitializer.InitializeLevel(cameraFollowInstance, gameUIInstance, _movementPlayer,
                gameObject.transform);
        }

        public void FixedTick() => _movementPlayer.FixedTick();


        public void ExitState()
        {
            _handlerLose.Unsubscribe();
            _levelInitializer.DestroyObject();
            _gameUIFactory.DestroyGameUI();
            _switchingService.UnsubscribeElements();
            _advertisingShow.DisposeShow();
            _cameraFollowFactory.DestroyInstance();
        }
    }
}