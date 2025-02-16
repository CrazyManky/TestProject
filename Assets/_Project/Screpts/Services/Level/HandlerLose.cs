using System.Collections.Generic;
using _Project._Screpts.GameStateMashine;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.GameStateMashine.States;
using Zenject;

namespace _Project._Screpts.Services
{
    public class HandlerLose
    {
        private GameFSM _gameFsm;
        private readonly List<MoveObject> _subscribedObjects = new(2);

        [Inject]
        public void Construct(GameFSM gameFsm)
        {
            _gameFsm = gameFsm;
        }

        public void Subscribe(MoveObject moveObject)
        {
            if (!_subscribedObjects.Contains(moveObject))
            {
                moveObject.OnDead += LoseGame;
                _subscribedObjects.Add(moveObject);
            }
        }
        
        public void UnsubscribeAll()
        {
            foreach (var moveObject in _subscribedObjects)
            {
                moveObject.OnDead -= LoseGame;
            }

            _subscribedObjects.Clear();
        }

        private void LoseGame()
        {
            _gameFsm.Enter<GameOverState>();
        }
    }
}