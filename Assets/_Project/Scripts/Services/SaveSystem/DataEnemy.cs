using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    public class DataEnemy
    {
        public string KeyItem { get; }
        public bool IsActive;
        public string Position;

        public DataEnemy(string keyItem, bool isActive, Vector3 position)
        {
            KeyItem = keyItem;
            IsActive = isActive;
            Position = JsonUtility.ToJson(position);
        }
    }
}