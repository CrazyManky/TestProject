using System;

namespace _Project.Scripts.Services.PauseSystem
{
    public class PauseService
    {
        public bool Pause { get; private set; } = false;
        public event Action OnPauseActive;
        public event Action OnPauseDisable;

        public void PauseActive()
        {
            Pause = true;
            OnPauseActive?.Invoke();
        }

        public void PauseDisable()
        {
            Pause = false;
            OnPauseDisable?.Invoke();
        }
    }
}