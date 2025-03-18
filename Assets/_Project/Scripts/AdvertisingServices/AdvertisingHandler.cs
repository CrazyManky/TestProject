using _Project.Scripts.Services.PauseSystem;
using _Project.Scripts.ShopSystem;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.AdvertisingServices
{
    public class AdvertisingHandler : IUnityAdsShowListener, IAdvertisingShow
    {
        private string _lodingAdID = "Interstitial_Android";
        private IPurchaseItem _purchaseItem;
        private bool _canWatch = true;
        private PauseService _pauseService;

        [Inject]
        public void Construct(IPurchaseItem purchaseItem, PauseService pauseService)
        {
            _purchaseItem = purchaseItem;
            _pauseService = pauseService;
        }

        public void Initialize()
        {
            var value = PlayerPrefs.GetInt("No-Ads");
            if (value == 1)
            {
                _canWatch = false;
                return;
            }

            _purchaseItem.OnPurchaseNoAdsComplete += DisableShowAds;
        }


        public void Show()
        {
            if (!_canWatch)
                return;

            if (Advertisement.isInitialized && !Advertisement.isShowing)
            {
                Advertisement.Show(_lodingAdID, this);
                _pauseService.PauseActive();
            }
        }


        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("OnUnityAdsShowComplete");
            _pauseService.PauseDisable();
        }

        private void DisableShowAds()
        {
            _canWatch = false;
            PlayerPrefs.SetInt("No-Ads", 1);
        }

        public void DisposeShow() => _purchaseItem.OnPurchaseNoAdsComplete -= DisableShowAds;

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message){}

        public void OnUnityAdsShowStart(string placementId){}

        public void OnUnityAdsShowClick(string placementId){}
    }
}