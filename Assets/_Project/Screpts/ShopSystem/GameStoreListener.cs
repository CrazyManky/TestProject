using System;
using _Project.Screpts.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace _Project.Screpts.ShopSystem
{
    public class GameStoreListener : IPurchaseItem, IControllerBuyItem
    {
        private IStoreController _storeController;
        public IStoreController StoreController => _storeController;
        
        public event Action OnPurchaseNoAdsСomplete;

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            Debug.Log("Game Store Initialized Successfully");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError("Game Store Initialization Failed,message: " + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            OnPurchaseNoAdsСomplete?.Invoke();
            return PurchaseProcessingResult.Complete;
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