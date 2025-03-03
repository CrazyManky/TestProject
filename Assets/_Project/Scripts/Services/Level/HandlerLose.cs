using _Project.Screpts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameStateMachine;
using _Project.Scripts.GameStateMachine.States;
using Zenject;

namespace _Project.Scripts.Services.Level
{
    public class HandlerLose
    {
        private GameFSM _gameFsm;
        private IShowReward _showReward;
        private IAnalytics _analytics;

        [Inject]
        public void Construct(GameFSM gameFsm, IShowReward showReward, IAnalytics analytics)
        {
            _gameFsm = gameFsm;
            _showReward = showReward;
            _analytics = analytics;
        }

        public void Subscribe() => _showReward.OnFeiledShow += LoseGame;

        public void Unsubscribe() => _showReward.OnFeiledShow += LoseGame;

        private void LoseGame() => _gameFsm.Enter<GameOverState>();
    }
}