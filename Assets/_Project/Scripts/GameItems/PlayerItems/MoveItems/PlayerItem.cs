using System;
using _Project.Scripts.AdvertisingServices;
using _Project.Scripts.AnalyticsService;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.LoadSystem.LoaderEntity;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.PlayerItems.MoveItems
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerItem : MonoBehaviour, IDamageProvider, ISaveData, ILoadingEntity, IDestroy
    {
        [SerializeField] private string _keyItem;

        private PlayerItemData PlayerItemData = new();
        private IConfigHandler _configHandler;
        private IShowReward _showReward;
        protected IAnalytics Analytics;
        private IDataProvider _dataProvider;
        private CharacterController _controller;
        private bool _iDead = false;

        public string Key => _keyItem;
        public int Health => PlayerItemData.Health;

        public bool IActive { get; private set; } = true;
        public event Action<int> OnHealthChanged;

        [Inject]
        public void Construct(IConfigHandler configHandler, IShowReward showReward, IAnalytics analytics,
            IDataProvider dataProvider)
        {
            _configHandler = configHandler;
            _showReward = showReward;
            Analytics = analytics;
            _dataProvider = dataProvider;
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            LoadConfig();
        }

        public void OnEnable() => _showReward.OnCompletedShow += Reset;

        private void LoadConfig()
        {
            var config = _configHandler.GetConfig<GameObjectConfig>(_keyItem);
            PlayerItemData.Health = config.Health;
            PlayerItemData.Speed = config.Speed;
            PlayerItemData.MaxHealth = config.MaxHealth;
        }

        public void Load()
        {
            var data = _dataProvider.GetData<SaveData>(Key);
            PlayerItemData.Health = data.Health;
            PlayerItemData.MaxHealth = data.Health;
            transform.position = data.Position;
        }

        public void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            _controller.Move(direction * PlayerItemData.Speed);
        }

        public void SetPosition(Vector3 position)
        {
            _controller.enabled = false;
            transform.position = position;
            _controller.enabled = true;
        }

        public void TakeDamage(int damage)
        {
            PlayerItemData.Health = Math.Max(0, PlayerItemData.Health - damage);
            OnHealthChanged?.Invoke(PlayerItemData.Health);
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (PlayerItemData.Health <= 0 && !_iDead)
            {
                _showReward.ActiveReward();
                _iDead = true;
            }
        }

        public void Reset()
        {
            _iDead = false;
            PlayerItemData.Health = PlayerItemData.MaxHealth;
            OnHealthChanged?.Invoke(PlayerItemData.Health);
        }

        public object SaveData()
        {
            var data = new SaveData(Key, PlayerItemData.Health, transform.position);
            return data;
        }

        public virtual void DisableItem()
        {
            IActive = false;
            _showReward.OnCompletedShow -= Reset;
            gameObject.SetActive(false);
            Analytics.NotifyPlayerDead(Key);
        }
    }
}