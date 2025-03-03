using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.LoadSystem.ConfigLoading
{
    public interface IConfigHandler
    {
        public  UniTask DownloadAsync();
        public T GetConfig<T>(string key);
    }
}