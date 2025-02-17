using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Screpts.AdvertisingServices
{
    public class UnityAdsHandler : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string AndroidGameId = "5797011";
        private string _gameId;
        private bool _testMode = true;

        void Awake()
        {
            InitializeAds();
        }

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
    }
}