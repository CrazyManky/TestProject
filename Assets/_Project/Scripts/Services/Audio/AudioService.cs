using System;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Audio
{
    [Serializable]
    public class AudioService : MonoBehaviour, IPlaySound
    {
        [SerializeField] private AudioSource _shotSound;
        [SerializeField] private AudioSource _exitZoneSound;
        [SerializeField] private AudioSource _backgroundSound;

        private PauseService _pauseService;

        [Inject]
        public void Construct(PauseService pauseService) => _pauseService = pauseService;
        
        private void OnEnable() => _pauseService.OnValueChange += Mute;
        
        public void PlayEnemyShotSound() => SetSoundState(_shotSound);
        public void PlayCollisionExitZone() => SetSoundState(_exitZoneSound);
        public void PlayBackgroundSound() => SetSoundState(_backgroundSound);

        private void SetSoundState(AudioSource source) => source.Play();

        private void Mute(bool value)
        {
            if (value)
            {
                _backgroundSound.mute = value;
                _shotSound.mute = value;
                _exitZoneSound.mute = value;
            }
            else
            {
                _backgroundSound.mute = value;
                _shotSound.mute = value;
                _exitZoneSound.mute = value;
            }
        }
        
        private void OnDisable() => _pauseService.OnValueChange -= Mute;
    }
}