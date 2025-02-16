using _Project.Screpts.Interfaces;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace _Project.Screpts.Analytics_Service
{
    public class FirebaseWrapper : IAnalytics
    {
        private FirebaseApp _app;

        public async UniTask Initialize()
        {
            Debug.Log("🔥 Начало инициализации Firebase...");
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance; // ✅ Создаем экземпляр Firebase
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Debug.Log("✅ Firebase успешно инициализирован");
            }
            else
            {
                Debug.LogError($"❌ Firebase не инициализирован: {dependencyStatus}");
                return;
            }

            Application.quitting += InvokeAppClose;
        }

        public void InvokeAppOpen()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
        }

        public void InvokeAppClose()
        {
            FirebaseAnalytics.LogEvent("App Closed");
            Application.quitting -= InvokeAppClose;
        }
    }
}