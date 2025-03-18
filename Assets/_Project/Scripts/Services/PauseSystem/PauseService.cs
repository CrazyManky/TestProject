using System;

namespace _Project.Scripts.Services.PauseSystem
{
    public class PauseService
    {
        public bool Pause { get; private set; } = false;
        public event Action<bool> OnValueChange;

        public void PauseActive()
        {
            Pause = true;
            OnValueChange?.Invoke(Pause);
        }

        public void PauseDisable()
        {
            Pause = false;
            OnValueChange?.Invoke(Pause);
        }
    }
}