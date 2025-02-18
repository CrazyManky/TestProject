using System;
using UnityEngine.Purchasing;

namespace _Project.Screpts.Interfaces
{
    public interface IPurchaseItem
    {
        public event Action OnPurchaseNoAdsСomplete;
    }
}