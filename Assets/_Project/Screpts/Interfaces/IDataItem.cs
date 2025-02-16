using UnityEngine;

namespace _Project._Screpts.Interfaces
{
    public class IDataItem
    {
        public string Key { get; }
        public int Health;
        public int MaxHealth;
        private Vector3 Position;
    }
}