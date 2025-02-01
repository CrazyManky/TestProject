using System;
using System.Collections.Generic;
using _Project._Screpts.GameItems.GameLevels.Levels;
using UnityEngine;


namespace _Project._Screpts.GameItems.GameLevels
{
    [Serializable]
    public class ConteinerLevels 
    {
        [SerializeField] private List<BaseLevel> _levels;

        private int _levelIndex;

        public BaseLevel GetLevel()
        {
            return _levels[_levelIndex];
        }
    }
}