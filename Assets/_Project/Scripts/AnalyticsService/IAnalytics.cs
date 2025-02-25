using Cysharp.Threading.Tasks;

namespace _Project.Scripts.AnalyticsService
{
    public interface IAnalytics
    {
        public  UniTask Initialize();
        public void NotifyAppOpen();
        public void NotifyPlayerDead(string itemKey);
        public void NotifyExitArea();
        public void NotifyLevelCompleted();
    }
}