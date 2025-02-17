using System;
using _Project.Screpts.Interfaces;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Screpts.AdvertisingServices
{
    public class UnityAdsHandler :  IUnityAdsInitializationListener,IUnityAdsShowListener,IAdsInitializer
    {
        private const string AndroidGameId = "5797011";
        private string _gameId;
        private bool _testMode = true;

        public void InitializeAds()
        {
#if UNITY_ANDROID
            _gameId = AndroidGameId;
#elif UNITY_EDITOR
            _gameId = AndroidGameId; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
        }


        public void OnInitializationComplete() => Debug.Log("Unity Ads initialization complete");


        public void OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Debug.Log("Unity Ads Initialization Failed: " + message);

        public void ShowAdd()
        {
            Advertisement.Show(_gameId, this);
        }
        
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
           // throw new NotImplementedException();
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            //throw new NotImplementedException();
        }

        public void OnUnityAdsShowClick(string placementId)
        {
           // throw new NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
          //  throw new NotImplementedException();
        }
    }
}