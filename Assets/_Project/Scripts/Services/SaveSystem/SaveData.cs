using System;
using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    [Serializable]
    public struct SaveData 
    {
        public string KeyItem { get; }
        public int Health;
        public string Position;


        public SaveData(string keyItem, int health, Vector3 position)
        {
            KeyItem = keyItem;
            Health = health;
            Position = JsonUtility.ToJson(position);
        }
    }
}