using System;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    [Serializable]
    public struct SaveData
    {
         public string KeyItem;
         public int Health;
         public Vector3 Position;


        public SaveData(string keyItem, int health, Vector3 position)
        {
            KeyItem = keyItem;
            Health = health;
            Position = position;
        }
    }
}