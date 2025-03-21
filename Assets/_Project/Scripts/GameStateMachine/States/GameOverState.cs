﻿using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.GameStateMachine.States
{
    public class GameOverState : IGameState
    {
        private GameFSM _gameStateMachine;
        private PlayerObjectCollector _playerObjectCollector;
        private IShowReward _showReward;
        private GameObjectDestroyer _destroyer;

        [Inject]
        public void Construct(GameFSM gameStateMachine, PlayerObjectCollector playerObjectCollector,
            IShowReward showReward, GameObjectDestroyer destroyer)
        {
            _gameStateMachine = gameStateMachine;
            _playerObjectCollector = playerObjectCollector;
            _showReward = showReward;
            _destroyer = destroyer;
        }

        public void EnterState()
        {
            _destroyer.DestroyItems();
            _playerObjectCollector.RemoveItems();
            _showReward.ResetCount();
            _gameStateMachine.Enter<GamePlayState>();
        }

        public void ExitState()
        {
        }
    }
}