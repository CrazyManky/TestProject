using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Screpts.ShopSystem
{
    public class GameStoreInitialize : IStoreInitialize, IBuyStoreItem
    {
        private IStoreController _controller;
        private IControllerBuyItem _listener;
        private const string _productNoAds = "No-Ads";

        [Inject]
        public void Construct(IControllerBuyItem listener)
        {
            _listener = listener;
        }

        public void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(_productNoAds, ProductType.NonConsumable);
            UnityPurchasing.Initialize(_listener, builder);
            _controller = _listener.StoreController;
        }

        public void BuyNoAds()
        {
            if (_controller != null)
                _controller.InitiatePurchase(_productNoAds);
            else
                Debug.LogError("Инициализация магазина не завершена!");
        }
    }
}