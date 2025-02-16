using System;
using _Project._Screpts.Interfaces;
using _Project._Screpts.SaveSystem;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameItems.PlayerObjects.MoveItems
{
    public abstract class MoveObject : MonoBehaviour, IDamageProvaider, ISaveAndLoad, IDestroyGameElement
    {
        [SerializeField] private string _keyItem;

        private IConfigHandler _configHandler;

        protected MoveableObjectData MoveableObjectData;

        public string KeyItem => _keyItem;
        public int Health => MoveableObjectData.Health;
        public int MaxHealth => MoveableObjectData.MaxHealth;

        public event Action OnDead;
        public event Action<int, int> OnValueChanged;

        [Inject]
        public void Construct(IConfigHandler configHandler) => _configHandler = configHandler;

        private void Awake() => LoadingConfig();

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
                OnDead?.Invoke();
        }


        public ISavableData SaveData()
        {
            var data = new SaveData(KeyItem, MoveableObjectData.Health, transform.position);
            return data;
        }


        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}