using System;
using _Project._Screpts.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project._Screpts.SaveSystem
{
    [Serializable]
    public struct SaveData : ISavableData
    {
        public string KeyItem { get; }
        public int Health;
        public Vector3Save Position;


        public SaveData(string keyItem, int health, Vector3 position)
        {
            KeyItem = keyItem;
            Health = health;
            Position = new Vector3Save(position.x, position.y, position.z);
        }
    }
}