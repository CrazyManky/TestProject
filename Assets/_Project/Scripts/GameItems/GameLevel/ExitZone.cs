using System;
using _Project.Screpts.Services;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.GameLevel
{
    public class ExitZone : MonoBehaviour
    {
        private PlayerObjectCollector _playerObjectCollector;
        private SwitchingService _switchingService;
        private IAnalytics _analytics;
        public event Action OnEnterObject;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, SwitchingService switchingService,
            IAnalytics analytics)
        {
            _playerObjectCollector = playerObjectCollector;
            _switchingService = switchingService;
            _analytics = analytics;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerItem moveObject))
            {
                _analytics.NotifyExitArea();
                OnEnterObject?.Invoke();
                _playerObjectCollector.RemoveItem(moveObject);
                _switchingService.SwitchObject();
            }
        }
    }
}