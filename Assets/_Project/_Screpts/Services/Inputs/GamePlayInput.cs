using System;
using System.Collections.Generic;
using _Project._Screpts.Interfaces;
using UnityEngine;

namespace Services
{
    public class GamePlayInput : IInputGamePlayActionHandler
    {
        private Dictionary<KeyCode, Action> _gamePlayActions;


        public GamePlayInput()
        {
            _gamePlayActions = new Dictionary<KeyCode, Action>();
        }

        public void AddActionGamePlay(KeyCode valueKey, Action action)
        {
            _gamePlayActions.Add(valueKey, action);
        }

        public void RemoveActionGamePlay(KeyCode valueKey)
        {
            _gamePlayActions.Remove(valueKey);
        }


        public void HandleInput()
        {
            foreach (var keyAction in _gamePlayActions)
            {
                if (Input.GetKeyDown(keyAction.Key))
                {
                    keyAction.Value.Invoke();
                    break;
                }
            }
        }
    }
}