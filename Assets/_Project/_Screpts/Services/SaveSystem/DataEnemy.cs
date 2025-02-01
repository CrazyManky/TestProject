using _Project._Screpts.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project._Screpts.SaveSystem
{
    public struct DataEnemy : ISavableData
    {
        public string KeyItem { get; }
        public bool IsActive;
        public Vector3Save Position;

        public DataEnemy(string keyItem, bool isActive, Vector3 position)
        {
            KeyItem = keyItem;
            IsActive = isActive;
            Position = new Vector3Save(position.x, position.y, position.z);
        }
    }
}