using System;

namespace _Project.Screpts.AdvertisingServices
{
    public interface IShowReward
    {
        public event Action OnCompletedShow;
        public event Action OnFeiledShow;
        public void ActiveReward();
    }
}