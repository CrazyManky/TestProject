using Cysharp.Threading.Tasks;

namespace _Project.Screpts.Services.LoadSystem.ConfigLoading
{
    public interface IConfigHandler
    {
        public  UniTask DownloadAsync();
        public T GetConfig<T>(string key) where T : class, IGameConfig;
    }
}