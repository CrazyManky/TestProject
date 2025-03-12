using System;
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

        [Inject]
        public void Construct(IAdvertisingShow advertisingShow) => _advertisingShow = advertisingShow;

        public void ActiveReward()
        {
            if (_countInvoke < _maxCountInvoke && !Advertisement.isShowing)
            {
                _countInvoke++;
                Advertisement.Show(_rewardID, this);
                return;
            }

            OnFieldShow?.Invoke();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            OnFieldShow?.Invoke();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == _rewardID && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                OnCompletedShow?.Invoke();
            else
                OnFieldShow?.Invoke();
        }

        public void ResetCount() => _countInvoke = 0;

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }
    }
}