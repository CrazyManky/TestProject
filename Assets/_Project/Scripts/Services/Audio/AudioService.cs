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
        public void Construct(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        private void OnEnable()
        {
            _pauseService.OnPauseActive += Mute;
            _pauseService.OnPauseDisable += Continue;
        }


        public void PlayEnemyShotSound() => SetSoundState(_shotSound);
        public void PlayCollisionExitZone() => SetSoundState(_exitZoneSound);
        public void PlayBackgroundSound() => SetSoundState(_backgroundSound);

        private void SetSoundState(AudioSource source) => source.Play();

        private void Mute() => _backgroundSound.Stop();
        private void Continue() => _backgroundSound.Play();

        private void OnDisable()
        {
            _pauseService.OnPauseActive -= Mute;
            _pauseService.OnPauseDisable -= Continue;
        }
    }
}