using System;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.Services;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.GameItems.GameLevels.Levels
{
    public class ExitZone : MonoBehaviour
    {
        private PlayerObjectCollector _playerObjectCollector;
        private SwitchObjectService _switchObjectService;

        public event Action OnEnterObject;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, SwitchObjectService switchObjectService)
        {
            _playerObjectCollector = playerObjectCollector;
            _switchObjectService = switchObjectService;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MoveObject moveObject))
            {
                OnEnterObject?.Invoke();
                _switchObjectService.SwitchObject();
                _playerObjectCollector.RemoveItem(moveObject);
            }
        }
    }
}