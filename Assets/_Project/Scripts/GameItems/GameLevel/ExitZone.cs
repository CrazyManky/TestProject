using System;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameItems.GameLevel
{
    public class ExitZone : MonoBehaviour
    {
        private PlayerObjectCollector _playerObjectCollector;
        private SwitchingService _switchingService;

        public event Action OnEnterObject;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, SwitchingService switchingService)
        {
            _playerObjectCollector = playerObjectCollector;
            _switchingService = switchingService;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MoveObject moveObject))
            {
                OnEnterObject?.Invoke();
                _playerObjectCollector.RemoveItem(moveObject);
                moveObject.DisableItem();
                _switchingService.SwitchObject();
            }
        }
    }
}