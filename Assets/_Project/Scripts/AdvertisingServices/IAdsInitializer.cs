using Cysharp.Threading.Tasks;

namespace _Project.Scripts.AdvertisingServices
{
    public interface IAdsInitializer
    {
        public UniTask InitializeAdsAsync();
    }
}