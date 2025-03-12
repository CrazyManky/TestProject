using System;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameItems.GameEffects;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Audio;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.GameLevel
{
    public class ExitZone : MonoBehaviour
    {
        [SerializeField] private AnnihilationEffect _annihilationEffect;

        private PlayerObjectCollector _playerObjectCollector;
        private SwitchingService _switchingService;
        private IAnalytics _analytics;
        private IPlaySound _soundPlayer;
        private Vector3 _offset = new(0, -0.7f, 0);
        public event Action OnEnterObject;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, SwitchingService switchingService,
            IAnalytics analytics, IPlaySound soundPlayer)
        {
            _playerObjectCollector = playerObjectCollector;
            _switchingService = switchingService;
            _analytics = analytics;
            _soundPlayer = soundPlayer;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerItem moveObject))
            {
                _analytics.NotifyExitArea();
                OnEnterObject?.Invoke();
                _playerObjectCollector.RemoveItem(moveObject);
                _switchingService.SwitchObject();
                _soundPlayer.PlayCollisionExitZone(true);
                ShowCollisionEffect(moveObject.transform.position);
            }
        }

        private void ShowCollisionEffect(Vector3 CollisionPosition)
        {
            var instance = Instantiate(_annihilationEffect);
            instance.transform.position = CollisionPosition + _offset;
        }
    }
}