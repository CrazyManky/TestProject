using System;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.Services.SaveSystem;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.PlayerItems.MoveItems
{
    public abstract class PlayerItem : MonoBehaviour, IDamageProvider, ISaveData, IDestroy
    {
        [SerializeField] private string _keyItem;

        private IConfigHandler _configHandler;
        private IShowReward _showReward;
        protected IAnalytics Analytics;
        protected PlayerItemData PlayerItemData;

        public string Key => _keyItem;
        public int Health => PlayerItemData.Health;
        public int MaxHealth => PlayerItemData.MaxHealth;
        public bool IActive = true;

        public event Action OnDead;
        public event Action<int, int> OnValueChanged;

        [Inject]
        public void Construct(IConfigHandler configHandler, IShowReward showReward, IAnalytics analytics)
        {
            _configHandler = configHandler;
            _showReward = showReward;
            Analytics = analytics;
        }

        private void Awake() => LoadingConfig();

        public void OnEnable() => _showReward.OnCompletedShow += Reset;

        public void LoadingConfig()
        {
            var config = _configHandler.GetConfig(_keyItem);

            if (config is GameObjectConfig gameObjectConfig)
            {
                PlayerItemData.Health = gameObjectConfig.Health;
                PlayerItemData.MaxHealth = gameObjectConfig.MaxHealth;
                PlayerItemData.Speed = gameObjectConfig.Speed;
            }
        }

        public abstract void Move(Vector3 direction);

        public abstract void SetPosition(Vector3 position);

        public void TakeDamage(int damage)
        {
            PlayerItemData.Health = Math.Max(0, PlayerItemData.Health - damage);
            OnValueChanged?.Invoke(PlayerItemData.Health, PlayerItemData.MaxHealth);
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (PlayerItemData.Health <= 0)
                _showReward.ActiveReward();
        }

        public void Reset()
        {
            PlayerItemData.Health = PlayerItemData.MaxHealth;
            OnValueChanged?.Invoke(PlayerItemData.Health, PlayerItemData.MaxHealth);
        }

        public void Load<T>(T data)
        {
            if (data is SaveData playerItemData)
            {
                PlayerItemData.Health = playerItemData.Health;
                transform.position = JsonConvert.DeserializeObject<Vector3>(playerItemData.Position);
                OnValueChanged?.Invoke(PlayerItemData.Health, PlayerItemData.MaxHealth);
            }
        }


        public object SaveData()
        {
            var data = new SaveData(Key, PlayerItemData.Health, transform.position);
            return data;
        }

        public virtual void DisableItem()
        {
            _showReward.OnCompletedShow -= Reset;
            gameObject.SetActive(false);
            IActive = false;
        }
    }
}