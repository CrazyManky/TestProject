using System;

namespace _Project.Scripts.Services.PauseSystem
{
    public class PauseService
    {
        public bool Pause { get; private set; } = false;
        public event Action OnPause;
        
        
        public void PauseActive()
        {
            Pause = true;
            OnPause?.Invoke();
        }

        public void PauseDisable()
        {
            Pause = false;
            OnPause?.Invoke();
        }

        private void SetValue(bool value) => Pause = value;
    }
}