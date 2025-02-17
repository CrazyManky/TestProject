using System;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Screpts.AdvertisingServices
{
    public class AdvertisingShowReward : IUnityAdsShowListener, IShowReward
    {
        private string _rewardID = "Rewarded_Android";

        public event Action OnCompletedShow;
        public event Action OnFeiledShow;

        private IAdvertisingShow _advertisingShow;

        [Inject]
        public void Conststruct(IAdvertisingShow advertisingShow)
        {
            _advertisingShow = advertisingShow;
        }

        public void ActiveReward()
        {
            if (Advertisement.isShowing)
                return;

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
                OnCompletedShow?.Invoke();
            else
            {
                OnFeiledShow?.Invoke();
                _advertisingShow.Show();
            }
        }
    }
}