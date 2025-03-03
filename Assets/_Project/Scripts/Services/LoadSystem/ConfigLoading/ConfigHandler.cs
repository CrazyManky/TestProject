using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
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
        private Dictionary<string, object> _parsedConfigs = new();

        public async UniTask DownloadAsync()
        {
            _configInstance = FirebaseRemoteConfig.DefaultInstance;
            await _configInstance.FetchAsync(TimeSpan.Zero);
            await _configInstance.ActivateAsync();

            _downloadConfigs = _configInstance.AllValues.ToDictionary(
                item => item.Key,
                item => item.Value.StringValue
            );

            Debug.Log("✅ Конфиги загружены успешно!");
        }

        private T DeserializeConfigByKey<T>(string jsonValue) => JsonConvert.DeserializeObject<T>(jsonValue);

        public T GetConfig<T>(string key)
        {
            if (_parsedConfigs.ContainsKey(key))
            {
                return (T)_parsedConfigs[key];
            }

            if (_downloadConfigs.TryGetValue(key, out string jsonValue))
            {
                T config = DeserializeConfigByKey<T>(jsonValue);
                _parsedConfigs[key] = config;
                return config;
            }

            Debug.LogWarning($"Конфиг с ключом '{key}' не найден.");
            return default(T);
        }
    }
}


public class GameObjectConfig
{
    public string KeyItem { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }
}

public class EnemyPathConfig
{
    public string KeyItem { get; set; }
    public int PatrolDistance { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
}

public class EnemyStalkerConfig
{
    public string KeyItem { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }
}

public class ShootingEnemyConfig
{
    public string KeyItem { get; set; }
    public int TurnSpeed { get; set; }
    public int ShotDelay { get; set; }
    public int ShootDistance { get; set; }
}