using System;
using _Project.Screpts.GameItems.PlayerObjects;
using _Project.Screpts.UI;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.MoveItems;
using Zenject;

namespace _Project.Screpts.Services
{
    public class SwitchingService
    {
        private PlayerObjectCollector _playerObjectCollector;
        private CameraFollow _cameraFollow;
        private MovementPlayer _movementPlayer;
        private GameUI _gameUI;
        public event Action<PlayerItem> OnSwitched;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector)
        {
            _playerObjectCollector = playerObjectCollector;
        }

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