using System;

namespace _Project.Scripts.ShopSystem
{
    public interface IPurchaseItem
    {
        public event Action OnPurchaseNoAdsComplete;
    }
}