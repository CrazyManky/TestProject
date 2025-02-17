using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Screpts.AdvertisingServices
{
    public class AdvertisingShow : IUnityAdsShowListener, IAdvertisingShow
    {
        private string _lodingAdID = "Interstitial_Android";

        public void Show()
        {
            if (Advertisement.isInitialized && !Advertisement.isShowing)
                Advertisement.Show(_lodingAdID, this);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            //  throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            // throw new System.NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("OnUnityAdsShowComplete");
        }
    }

    public interface IAdvertisingShow
    {
        public void Show();
    }
}