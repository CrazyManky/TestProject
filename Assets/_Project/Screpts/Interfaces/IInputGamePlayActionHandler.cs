using System;
using UnityEngine;

namespace Services
{
    public interface IInputGamePlayActionHandler
    {
        public void HandleInput();
        public void AddActionGamePlay(KeyCode valueKey,Action action );
        public void RemoveActionGamePlay(KeyCode valueKey);
    }
}