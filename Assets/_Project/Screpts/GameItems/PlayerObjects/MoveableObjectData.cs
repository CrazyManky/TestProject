using System;
using _Project._Screpts.GameItems.Enemy;
using UnityEngine;

namespace _Project._Screpts.GameItems.PlayerObjects
{
    [Serializable]
    public struct MoveableObjectData 
    {
        [SerializeField] private string _keyItem;
        public string KeyItem => _keyItem;
        public int Health;
        public int MaxHealth;
    }
}