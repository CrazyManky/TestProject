using System;
using System.Collections.Generic;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.AdvertisingServices
{
    public class RewardHandler : IUnityAdsShowListener, IShowReward
    {
        private const string _rewardID = "Rewarded_Android";
        private const int _maxCountInvoke = 1;
        private int _countInvoke = 0;
        public event Action OnCompletedShow;
        public event Action OnFieldShow;

        private IAdvertisingShow _advertisingShow;
        private PauseService _pauseService;


        [Inject]
        public void Construct(IAdvertisingShow advertisingShow, PauseService pauseService)
        {
            _advertisingShow = advertisingShow;
            _pauseService = pauseService;
        }

        public void ActiveReward()
        {
            if (_countInvoke < _maxCountInvoke && !Advertisement.isShowing)
            {
                _countInvoke++;
                Advertisement.Show(_rewardID, this);
                _pauseService.PauseActive();
                return;
            }

            OnFieldShow?.Invoke();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            OnFieldShow?.Invoke();
            _pauseService.PauseDisable();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == _rewardID && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                OnCompletedShow?.Invoke();
            }
            else
                OnFieldShow?.Invoke();

            _pauseService.PauseDisable();
        }

        public void ResetCount() => _countInvoke = 0;

        public void OnUnityAdsShowStart(string placementId)
        {
            _pauseService.PauseActive();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }
    }
}