using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace _Project.Scripts.AnalyticsService
{
    public class EventHandler : IAnalytics
    {
        public async UniTask Initialize()
        {
            Debug.Log("🔥 Начало инициализации Firebase...");
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Debug.Log("✅ Firebase успешно инициализирован");
            }
            else
            {
                return;
            }

            Application.quitting += InvokeAppClose;
        }

        public void NotifyAppOpen() => FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);

        public void NotifyPlayerDead(string keyItem) => FirebaseAnalytics.LogEvent($"NotifyPlayerDead:{keyItem}");
        public void NotifyExitArea() => FirebaseAnalytics.LogEvent($"NotifyExitArea");
        public void NotifyLevelCompleted() => FirebaseAnalytics.LogEvent($"NotifyLevelCompleted");

        private void InvokeAppClose()
        {
            FirebaseAnalytics.LogEvent("App Closed");
            Application.quitting -= InvokeAppClose;
        }
    }
}