﻿using _Project.Screpts.Interfaces;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Screpts.AdvertisingServices
{
    public class AdvertisingShow : IUnityAdsShowListener, IAdvertisingShow
    {
        private string _lodingAdID = "Interstitial_Android";
        private IPurchaseItem _purchaseItem;
        private bool _canWatch = true;

        [Inject]
        public void Construct(IPurchaseItem purchaseItem) => _purchaseItem = purchaseItem;

        public void Initialize()
        {
            var value = PlayerPrefs.GetInt("No-Ads");
            if (value == 1)
            {
                _canWatch = false;
                return;
            }

            _purchaseItem.OnPurchaseNoAdsСomplete += DisableShowAds;
        }


        public void Show()
        {
            if (!_canWatch)
                return;

            if (Advertisement.isInitialized && !Advertisement.isShowing)
                Advertisement.Show(_lodingAdID, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("OnUnityAdsShowComplete");
        }

        private void DisableShowAds()
        {
            _canWatch = false;
            PlayerPrefs.SetInt("No-Ads", 1);
        }

        public void DisposeShow() => _purchaseItem.OnPurchaseNoAdsСomplete -= DisableShowAds;
    }
}