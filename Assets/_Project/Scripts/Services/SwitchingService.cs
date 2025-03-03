using System;
using _Project.Screpts.Services;
using _Project.Scripts.GameItems.PlayerItems;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.MoveItems;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Services
{
    public class SwitchingService
    {
        private PlayerObjectCollector _playerObjectCollector;
        private CameraFollow _cameraFollow;
        private MovementPlayer _movementPlayer;
        private GameUI _gameUI;
        public event Action<PlayerItem> OnSwitched;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector) =>
            _playerObjectCollector = playerObjectCollector;


        public void SubscribeElements(CameraFollow cameraFollow, MovementPlayer movementPlayer,
            GameUI gameUI)
        {
            _movementPlayer = movementPlayer;
            _gameUI = gameUI;
            _cameraFollow = cameraFollow;
            Subscribe();
        }

        private void Subscribe()
        {
            OnSwitched += _cameraFollow.SetTarget;
            OnSwitched += _movementPlayer.SetMovementItem;
            OnSwitched += _gameUI.SubscribeInObject;
        }


        public void SwitchObject()
        {
            var item = _playerObjectCollector.GetNewMoveObject();
            if (item != null)
                OnSwitched?.Invoke(item);
        }

        public void UnsubscribeElements()
        {
            OnSwitched -= _cameraFollow.SetTarget;
            OnSwitched -= _movementPlayer.SetMovementItem;
            OnSwitched -= _gameUI.SubscribeInObject;
        }
    }
}