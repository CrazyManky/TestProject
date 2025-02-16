using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project._Screpts.SaveSystem
{
    [Serializable]
    public struct LoadingItem
    {
        [JsonProperty("Health")] public int Health;
        [JsonProperty("Health")] public int MaxHealth;
        [JsonProperty("Health")] public Vector3 Position;
    }
}