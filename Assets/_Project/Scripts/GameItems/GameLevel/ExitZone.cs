using System;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameItems.GameEffects;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.GameLevel
{
    public class ExitZone : MonoBehaviour
    {
        [SerializeField] private AnnihilationEffect _annihilationEffect;
        [SerializeField] private AudioSource _audioSource;

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
                ShowCollisionEffect(moveObject.transform.position);
                _audioSource.Play();
            }
        }

        private void ShowCollisionEffect(Vector3 CollisionPosition)
        {
            var instance = Instantiate(_annihilationEffect);
            instance.transform.position = CollisionPosition;
        }
    }
}