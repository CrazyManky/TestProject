using System;
using _Project._Screpts.Services;
using _Project.Screpts.GameItems.PlayerObjects;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.Services.MoveItems;
using _Project.Screpts.UI;
using Zenject;

namespace _Project.Screpts.Services
{
    public class SwitchingService
    {
        private PlayerObjectCollector _playerObjectCollector;
        private CameraFollow _cameraFollow;
        private MovementPlayerObjects _movementPlayerObjects;
        private GameUI _gameUI;
        public event Action<MoveObject> OnSwitched;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector)
        {
            _playerObjectCollector = playerObjectCollector;
        }

        public void SubscribeElements(CameraFollow cameraFollow,MovementPlayerObjects movementPlayerObjects,GameUI gameUI)
        {
            _movementPlayerObjects = movementPlayerObjects;
            _gameUI = gameUI;
            _cameraFollow = cameraFollow;
            Subscribe();
        }

        private void Subscribe()
        {
            OnSwitched += _cameraFollow.SetTarget;
            OnSwitched += _movementPlayerObjects.SetMovementItem;
            OnSwitched += _gameUI.SubscribeInObject;
        }


        public void SwitchObject()
        {
            var item = _playerObjectCollector.GetNewMovebleObject();

            if (item is null)
                return;

            OnSwitched?.Invoke(item);
        }

        public void UnsubscribeElements()
        {
            OnSwitched -= _cameraFollow.SetTarget;
            OnSwitched -= _movementPlayerObjects.SetMovementItem;
            OnSwitched -= _gameUI.SubscribeInObject;
        }
    }
}