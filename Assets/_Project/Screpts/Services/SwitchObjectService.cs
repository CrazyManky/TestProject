using System;
using _Project._Screpts.GameItems.PlayerObjects;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.UI;
using _Project.Screpts.GameItems.PlayerObjects;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.Services.MoveItems;
using _Project.Screpts.UI;
using Zenject;

namespace _Project._Screpts.Services
{
    public class SwitchObjectService
    {
        private PlayerObjectCollector _playerObjectCollector;
        private CameraFollow _cameraFollow;
        private MovePlayerItems _movePlayerItems;
        private GameUI _gameUI;
        public event Action<MoveObject> OnSwitched;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector)
        {
            _playerObjectCollector = playerObjectCollector;
        }

        public void SubscribeElements(CameraFollow cameraFollow,MovePlayerItems movePlayerItems,GameUI gameUI)
        {
            _movePlayerItems = movePlayerItems;
            _gameUI = gameUI;
            _cameraFollow = cameraFollow;
            Subscribe();
        }

        private void Subscribe()
        {
            OnSwitched += _cameraFollow.SetTarget;
            OnSwitched += _movePlayerItems.SetMovementItem;
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
            OnSwitched -= _movePlayerItems.SetMovementItem;
            OnSwitched -= _gameUI.SubscribeInObject;
        }
    }
}