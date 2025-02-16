using Cysharp.Threading.Tasks;

namespace _Project.Screpts.Services.LoadSystem.ConfigLoading
{
    public interface IConfigHandler
    {
        public  UniTask DownloadAsync();
        public IGameConfig GetConfig(string key);
    }
}