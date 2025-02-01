using System.Collections.Generic;
using _Project._Screpts.GameItems.GameLevels.Levels;
using _Project._Screpts.Interfaces;
using UnityEngine;

namespace _Project.Screpts.GameItems.GameLevel
{
    public class GameLevel : MonoBehaviour, IDestroyGameElement
    {
        [SerializeField] private List<Transform> _pointsPlayerObject;
        [SerializeField] private List<Transform> _pointsEnemy;
        [SerializeField] private ExitZone _exitZone;

        private int _countPlayerObject = -1;
        private int _countEnemyObject = -1;

        public ExitZone ExitZone => _exitZone;

        public Vector3 GetPlayerPosition()
        {
            if (_countEnemyObject < _pointsPlayerObject.Count)
            {
                _countPlayerObject++;
                return _pointsPlayerObject[_countPlayerObject].position;
            }

            return Vector3.zero;
        }


        public Vector3 GetEnemyPosition()
        {
            if (_countEnemyObject < _pointsPlayerObject.Count)
            {
                _countEnemyObject++;
                return _pointsEnemy[_countEnemyObject].position;
            }

            return Vector3.zero;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}