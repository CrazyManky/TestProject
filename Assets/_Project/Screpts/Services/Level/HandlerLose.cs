using System.Collections.Generic;
using _Project._Screpts.GameStateMashine;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.GameStateMashine.States;
using Zenject;

namespace _Project._Screpts.Services
{
    public class HandlerLose
    {
        private GameFSM _gameFsm;
        private IShowReward _showReward;

        [Inject]
        public void Construct(GameFSM gameFsm, IShowReward showReward)
        {
            _gameFsm = gameFsm;
            _showReward = showReward;
        }

        public void Subscribe() => _showReward.OnFeiledShow += LoseGame;

        public void Unsubscribe() => _showReward.OnFeiledShow += LoseGame;

        private void LoseGame() => _gameFsm.Enter<GameOverState>();
    }
}