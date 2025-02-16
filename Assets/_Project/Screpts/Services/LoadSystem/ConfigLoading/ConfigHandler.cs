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
        private Dictionary<string, string> _downloadConfigs = new Dictionary<string, string>();
        private Dictionary<string, IGameConfig> _parsedConfigs = new();

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

                IGameConfig config = DeserializeConfigByKey(key, jsonValue);
                if (config != null)
                {
                    _parsedConfigs[key] = config;
                }
                else
                {
                    Debug.LogError($"❌ Не удалось десериализовать конфиг для ключа {key}");
                }
            }
        }

        private IGameConfig DeserializeConfigByKey(string key, string jsonValue)
        {
            switch (key)
            {
                case "Capsule":
                case "Cube":
                    return JsonConvert.DeserializeObject<GameObjectConfig>(jsonValue);
                case "EnemPathFollowing":
                    return JsonConvert.DeserializeObject<EnemyPathConfig>(jsonValue);
                case "StalkerEnemy":
                    return JsonConvert.DeserializeObject<EnemyStalkerConfig>(jsonValue);
                case "ShotingEnemy":
                    return JsonConvert.DeserializeObject<ShootingEnemyConfig>(jsonValue);
                default:
                    return null;
            }
        }


        public IGameConfig GetConfig(string key)
        {
            if (_parsedConfigs.ContainsKey(key))
            {
                Debug.Log($" выданный объект:{_parsedConfigs[key].KeyItem}");
                return _parsedConfigs[key];
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