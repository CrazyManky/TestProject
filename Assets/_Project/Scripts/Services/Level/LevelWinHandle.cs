using _Project.Screpts.GameStateMashine.States;
using _Project.Screpts.Services;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameStateMachine;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.Services.Level
{
    public class LevelWinHandle
    {
        private PlayerObjectCollector _playerObjectCollector;
        private GameFSM _gameFSM;
        private IAnalytics _analytics;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, GameFSM gameFSM,IAnalytics analytics)
        {
            _playerObjectCollector = playerObjectCollector;
            _gameFSM = gameFSM;
            _analytics = analytics;
        }


        public void CheckWin()
        {
            Debug.Log(_playerObjectCollector.ObjectCount);
            if (_playerObjectCollector.ObjectCount - 1 <= 0)
            {
                _analytics.NotifyLevelCompleted();
                _gameFSM.Enter<GameOverState>();
            }
            
        }
    }
}