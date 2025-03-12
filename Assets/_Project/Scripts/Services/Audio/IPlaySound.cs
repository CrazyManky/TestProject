﻿namespace _Project.Scripts.Services.Audio
{
    public interface IPlaySound
    {
        public void PlayEnemyShotSound(bool play);
        public void PlayCollisionExitZone(bool play);
        public void PlayBackgroundSound(bool play);
    }
}