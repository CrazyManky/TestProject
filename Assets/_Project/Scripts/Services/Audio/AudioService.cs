using System;
using UnityEngine;

namespace _Project.Scripts.Services.Audio
{
    [Serializable]
    public class AudioService : IPlaySound
    {
        [SerializeField] private AudioSource _shotSound;
        [SerializeField] private AudioSource _exitZoneSound;
        [SerializeField] private AudioSource _backgroundSound;

        public void PlayEnemyShotSound() => SetSoundState(_shotSound, true);
        public void PlayCollisionExitZone(bool play) => SetSoundState(_exitZoneSound, play);
        public void PlayBackgroundSound(bool play) => SetSoundState(_backgroundSound, play);

        private void SetSoundState(AudioSource source, bool play)
        {
            if (source == null) return;

            if (play) source.Play();
            else source.Stop();
        }
    }
}