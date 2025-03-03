using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    public class DataEnemy
    {
        public string KeyItem;
        public bool IsActive;
        public Vector3 Position;

        public DataEnemy(string keyItem, bool isActive, Vector3 position)
        {
            KeyItem = keyItem;
            IsActive = isActive;
            Position = position;
        }
    }
}