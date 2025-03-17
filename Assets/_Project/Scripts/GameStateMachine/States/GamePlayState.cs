using System.Collections.Generic;
using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.Inputs;
using _Project.Scripts.Services.Level;
using _Project.Scripts.Services.MoveItems;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameStateMachine.States
{
    public class GamePlayState : IGameState, IFixedTickable
    {
        private MovementPlayer _movementPlayer;
        private InputHandler _inputHandler;
        private LevelInitializer _levelInitializer;
        private SwitchingService _switchingService;
        private GameUIFactory _gameUIFactory;
        private CameraFollowFactory _cameraFollowFactory;
        private HandlerLose _handlerLose;
        private IAdvertisingShow _advertisingShow;
        private GameObject _gameObject = new("level");
        private IPlaySound _playSound;

        private List<GameObject> _destroyGameObject = new List<GameObject>();

        [Inject]
        public GamePlayState(InputHandler inputHandler, CameraFollowFactory cameraFollowFactory,
            LevelInitializer levelInitializer, MovementPlayer movementPlayer, GameUIFactory gameUIFactory,
            SwitchingService switchingService, HandlerLose handlerLose,
            IAdvertisingShow advertisingShow, IPlaySound playSound)
        {
            _inputHandler = inputHandler;
            _cameraFollowFactory = cameraFollowFactory;
            _levelInitializer = levelInitializer;
            _movementPlayer = movementPlayer;
            _gameUIFactory = gameUIFactory;
            _switchingService = switchingService;
            _handlerLose = handlerLose;
            _advertisingShow = advertisingShow;
            _playSound = playSound;
        }

        public void EnterState()
        {
            _handlerLose.Subscribe();
            InitGamePlay();
            _advertisingShow.Initialize();
            _advertisingShow.Show();
            _playSound.PlayBackgroundSound(true);
        }

        private void InitGamePlay()
        {
            var gameUIInstance = _gameUIFactory.InstanceGUI(_gameObject.transform);
            var cameraFollowInstance = _cameraFollowFactory.InstanceCameraFollow(_gameObject.transform);
            _switchingService.SubscribeElements(cameraFollowInstance, _movementPlayer, gameUIInstance);
            _levelInitializer.InitializeLevel(cameraFollowInstance, gameUIInstance, _movementPlayer,
                _gameObject.transform);
            _inputHandler.Initialize();
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
            Object.Destroy(_gameObject.gameObject);
        }
    }
}