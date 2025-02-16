using _Project._Screpts.GameStateMashine;
using _Project.Screpts.GameStateMashine.States;
using _Project.Screpts.Services;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.Services.Level
{
    public class LevelWinHandle
    {
        private PlayerObjectCollector _playerObjectCollector;
        private GameFSM _gameFSM;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, GameFSM gameFSM)
        {
            _playerObjectCollector = playerObjectCollector;
            _gameFSM = gameFSM;
        }


        public void CheckWin()
        {
            Debug.Log(_playerObjectCollector.ObjectCount);
            if (_playerObjectCollector.ObjectCount <= 1)
            {
                _gameFSM.Enter<GameOverState>();
            }
        }
    }
}