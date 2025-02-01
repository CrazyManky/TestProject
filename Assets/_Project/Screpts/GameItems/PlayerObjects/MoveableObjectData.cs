using System;
using UnityEngine;

namespace _Project.Screpts.GameItems.PlayerObjects
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