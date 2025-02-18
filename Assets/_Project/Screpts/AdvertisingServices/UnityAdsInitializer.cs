using System.Threading.Tasks;
using _Project.Screpts.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Screpts.AdvertisingServices
{
    public class UnityAdsInitializer : IUnityAdsInitializationListener, IAdsInitializer
    {
        private const string AndroidGameId = "5797011";
        private string _gameId;
        private bool _testMode = true;
        private TaskCompletionSource<bool> _initializationTask = new();

        public async UniTask InitializeAdsAsync()
        {
#if UNITY_ANDROID
        _gameId = AndroidGameId;
#elif UNITY_EDITOR
            _gameId = AndroidGameId; 
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
                await _initializationTask.Task;
            }
        }
        
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete");
            _initializationTask.TrySetResult(true); 
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Unity Ads initialization failed: " + message);
            _initializationTask.TrySetResult(false);
        }
    }
}