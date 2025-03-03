using System;

namespace _Project.Scripts.Services.PauseSystem
{
    public class PauseService
    {
        public bool Pause { get; private set; } = false;
        public event Action OnPause;

        public void PauseExecute()
        {
            SwitchValue();
            OnPause?.Invoke();
        }

        private void SwitchValue()
        {
            var value = !Pause;
            Pause = value;
        }
    }
}