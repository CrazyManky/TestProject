using System;

namespace _Project.Scripts.AdvertisingServices
{
    public interface IShowReward
    {
        public event Action OnCompletedShow;
        public event Action OnFieldShow;
        public void ActiveReward();

        public void ResetCount();
    }
}