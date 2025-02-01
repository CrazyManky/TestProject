using _Project._Screpts.Interfaces;
using _Project._Screpts.Services;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.Level;
using _Project._Screpts.Services.MoveItems;
using _Project._Screpts.Services.PauseSystem;
using Services;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.GameStateMashine.States
{
    public class GamePlayState : IGameState, IFixedTickable
    {
        private MovePlayerItems _movePlayerItems;
        private InputHandler _inputHandler;
        private PauseService _pauseService;
        private LevelInitializer _levelInitializer;
        private SwitchObjectService _switchObjectService;
        private GameUIFactory _gameUIFactory;
        private CameraFollowFactory _cameraFollowFactory;

        [Inject]
        public GamePlayState(InputHandler inputHandler, CameraFollowFactory cameraFollowFactory,
            LevelInitializer levelInitializer, MovePlayerItems movePlayerItems, GameUIFactory gameUIFactory,
            SwitchObjectService switchObjectService, PauseService pauseService)
        {
            _inputHandler = inputHandler;
            _cameraFollowFactory = cameraFollowFactory;
            _levelInitializer = levelInitializer;
            _movePlayerItems = movePlayerItems;
            _gameUIFactory = gameUIFactory;
            _switchObjectService = switchObjectService;
            _pauseService = pauseService;
        }

        public void EnterState()
        {
            InitGamePlay();
            AddActionInput();
        }

        private void InitGamePlay()
        {
            var cameraFollowConteiner = new GameObject("CameraFollow");
            var UIConteiner = new GameObject("UI");
            var gameUIInstance = _gameUIFactory.InstanceGUI(UIConteiner.transform);
            var cameraFollowInstance = _cameraFollowFactory.InstanceCameraFollow(cameraFollowConteiner.transform);
            _switchObjectService.SubscribeElements(cameraFollowInstance, _movePlayerItems, gameUIInstance);
            _levelInitializer.InitializeLevel(cameraFollowInstance, gameUIInstance, _movePlayerItems);
            _pauseService.SetUI(gameUIInstance);
        }


        private void AddActionInput()
        {
            _inputHandler.GamePlayInput.AddActionGamePlay(KeyCode.Tab, _switchObjectService.SwitchObject);
            _inputHandler.GamePlayInput.AddActionGamePlay(KeyCode.Escape, _pauseService.PauseExecute);
        }

        public void FixedTick() => _movePlayerItems.FixedTick();


        public void ExitState()
        {
            _cameraFollowFactory.DestroyCameraFollow();
            _inputHandler.GamePlayInput.RemoveActionGamePlay(KeyCode.Tab);
            _inputHandler.GamePlayInput.RemoveActionGamePlay(KeyCode.Escape);
            _levelInitializer.DestroyObject();
            _gameUIFactory.DestroyGameUI();
            _switchObjectService.UnsubscribeElements();
        }
    }
}