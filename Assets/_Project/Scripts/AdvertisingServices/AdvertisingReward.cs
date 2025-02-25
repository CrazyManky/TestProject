using System;
using _Project.Screpts.Interfaces;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Screpts.AdvertisingServices
{
    public class AdvertisingShowReward : IUnityAdsShowListener, IShowReward
    {
        private string _rewardID = "Rewarded_Android";
        private int _maxCountInvoke = 1;
        private int _countInvoke = 0;
        public event Action OnCompletedShow;
        public event Action OnFeiledShow;

        private IAdvertisingShow _advertisingShow;

        [Inject]
        public void Construct(IAdvertisingShow advertisingShow) => _advertisingShow = advertisingShow;
        
        public void ActiveReward()
        {
            if (Advertisement.isShowing || _countInvoke == _maxCountInvoke)
            {
                OnFeiledShow?.Invoke();
                return;
            }

            Advertisement.Show(_rewardID, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            OnFeiledShow?.Invoke();
            _advertisingShow.Show();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == _rewardID && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                _maxCountInvoke++;
                OnCompletedShow?.Invoke();
            }
            else
            {
                OnFeiledShow?.Invoke();
                _advertisingShow.Show();
            }
        }

        public void ResetCount() => _countInvoke = 0;
    }
}