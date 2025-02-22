﻿using System;
using _Project._Screpts.Interfaces;
using _Project._Screpts.SaveSystem;
using _Project.Screpts.AdvertisingServices;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameItems.PlayerObjects.MoveItems
{
    public abstract class MoveObject : MonoBehaviour, IDamageProvider, ISaveAndLoad, IDestroyGameElement
    {
        [SerializeField] private string _keyItem;

        private IConfigHandler _configHandler;
        private IShowReward _showReward;
        protected MoveableObjectData MoveableObjectData;

        public string KeyItem => _keyItem;
        public int Health => MoveableObjectData.Health;
        public int MaxHealth => MoveableObjectData.MaxHealth;
        public bool IActive = true;

        public event Action OnDead;
        public event Action<int, int> OnValueChanged;

        [Inject]
        public void Construct(IConfigHandler configHandler, IShowReward showReward)
        {
            _configHandler = configHandler;
            _showReward = showReward;
        }

        private void Awake() => LoadingConfig();

        public void OnEnable() => _showReward.OnCompletedShow += Reset;

        public void LoadingConfig()
        {
            var config = _configHandler.GetConfig(_keyItem);

            if (config is GameObjectConfig gameObjectConfig)
            {
                MoveableObjectData.Health = gameObjectConfig.Health;
                MoveableObjectData.MaxHealth = gameObjectConfig.MaxHealth;
                MoveableObjectData.Speed = gameObjectConfig.Speed;
            }
        }

        public void Load(ISavableData data)
        {
            if (data is SaveData dataObject)
            {
                MoveableObjectData.Health = dataObject.Health;
                transform.position = new Vector3(dataObject.Position.X, dataObject.Position.Y, dataObject.Position.Z);
                OnValueChanged?.Invoke(MoveableObjectData.Health, MoveableObjectData.MaxHealth);
            }
        }

        public abstract void Move(Vector3 direction);

        public abstract void SetPosition(Vector3 position);

        public void TakeDamage(int damage)
        {
            MoveableObjectData.Health -= damage;
            if (MoveableObjectData.Health < 0)
                MoveableObjectData.Health = 0;


            OnValueChanged?.Invoke(MoveableObjectData.Health, MoveableObjectData.MaxHealth);
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (MoveableObjectData.Health <= 0)
                _showReward.ActiveReward();
        }

        public void Reset()
        {
            MoveableObjectData.Health = MoveableObjectData.MaxHealth;
            OnValueChanged?.Invoke(MoveableObjectData.Health, MoveableObjectData.MaxHealth);
        }

        public ISavableData SaveData()
        {
            var data = new SaveData(KeyItem, MoveableObjectData.Health, transform.position);
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