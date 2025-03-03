using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace _Project.Scripts.ShopSystem
{
    public class GameStoreListener : IPurchaseItem, IControllerBuyItem
    {
        private IStoreController _storeController;
        public IStoreController StoreController => _storeController;

        public event Action OnPurchaseNoAdsComplete;

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            Debug.Log("Game Store Initialized Successfully");
        }


        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError("Game Store Initialization Failed,message: " + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            OnPurchaseNoAdsComplete?.Invoke();
            return PurchaseProcessingResult.Complete;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
        }
    }

    public interface IControllerBuyItem : IDetailedStoreListener
    {
        public IStoreController StoreController { get; }
    }
}