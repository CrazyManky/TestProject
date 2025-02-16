using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Screpts.Services.LoadSystem.ConfigLoading
{
    public class ConfigHandler : IConfigHandler
    {
        private FirebaseRemoteConfig _configInstance;
        private Dictionary<string, string> _downloadConfigs = new();
        private Dictionary<string, IGameConfig> _parsedConfigs = new();

        // 🔹 Словарь для автоматического выбора типа по ключу
        private readonly Dictionary<string, Type> _configTypes = new()
        {
            { "Capsule", typeof(GameObjectConfig) },
            { "Cube", typeof(GameObjectConfig) },
            { "EnemPathFollowing", typeof(EnemyPathConfig) },
            { "ShotingEnemy", typeof(ShootingEnemyConfig) },
            { "StalkerEnemy", typeof(EnemyStalkerConfig) }
        };

        public async UniTask DownloadAsync()
        {
            try
            {
                _configInstance = FirebaseRemoteConfig.DefaultInstance;
                await _configInstance.FetchAsync(TimeSpan.Zero);
                await _configInstance.ActivateAsync();

                _downloadConfigs = _configInstance.AllValues.ToDictionary(
                    item => item.Key,
                    item => item.Value.StringValue
                );

                DeserializeData();

                Debug.Log("✅ Конфиги загружены успешно!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка загрузки конфига: {e}");
                throw;
            }
        }

        private void DeserializeData()
        {
            foreach (var entry in _downloadConfigs)
            {
                string key = entry.Key;
                string jsonValue = entry.Value;

                // 🔹 Проверяем, есть ли ключ в словаре типов
                if (_configTypes.TryGetValue(key, out Type configType))
                {
                    // Десериализуем JSON в нужный тип
                    IGameConfig config = JsonConvert.DeserializeObject(jsonValue, configType) as IGameConfig;

                    if (config != null)
                    {
                        _parsedConfigs[key] = config;
                    }
                    else
                    {
                        Debug.LogError($"❌ Ошибка десериализации для ключа {key}!");
                    }
                }
                else
                {
                    Debug.LogError($"❌ Неизвестный ключ конфигурации: {key}");
                }
            }
        }

        public T GetConfig<T>(string key) where T : class, IGameConfig
        {
            if (_parsedConfigs.TryGetValue(key, out IGameConfig config))
            {
                return config as T;
            }

            return null;
        }
    }
}

public class GameObjectConfig : IGameConfig
{
    public string KeyItem { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }
}

public class EnemyPathConfig : IGameConfig
{
    public string KeyItem { get; set; }
    public int PatrolDistance { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
}

public class EnemyStalkerConfig : IGameConfig
{
    public string KeyItem { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
}

public class ShootingEnemyConfig : IGameConfig
{
    public string KeyItem { get; set; }
    public int TurnSpeed { get; set; }
    public int ShotDelay { get; set; }
    public int ShootDistance { get; set; }
}