using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.Conteiner
{
    [Serializable]
    public class EnemyConteiner
    {
        [SerializeField] private List<EnemyComponents.Enemy> _baseEnemies = new();

        public EnemyComponents.Enemy GetItem(int index)
        {
            if (index < 0 || index >= _baseEnemies.Count)
                throw new IndexOutOfRangeException($"{this} is out of range.");
            
            return _baseEnemies[index];
        }
    }
}