using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.GameStateMachine;
using _Project.Scripts.GameStateMachine.States;
using _Project.Scripts.Services.Factory;
using Zenject;

namespace _Project.Scripts.Services.Level
{
    public class HandlerLose
    {
        private GameUIFactory _gameUIFactory;
        private IShowReward _showReward;


        [Inject]
        public void Construct(GameUIFactory gameUIFactory, IShowReward showReward)
        {
            _gameUIFactory = gameUIFactory;
            _showReward = showReward;
        }

        public void Subscribe() => _showReward.OnFieldShow += LoseGame;

        public void Unsubscribe() => _showReward.OnFieldShow -= LoseGame;

        private void LoseGame()
        {
            var gameOverScreen = _gameUIFactory.InstanceGameOverScreen();
            gameOverScreen.Open();
        }
    }
}