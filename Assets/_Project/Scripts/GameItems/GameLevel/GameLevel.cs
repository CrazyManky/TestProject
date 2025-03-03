using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameItems.GameLevel
{
    public class GameLevel : MonoBehaviour, IDestroy
    {
        [field: SerializeField] public ExitZone ExitZone { get; private set; }
        [SerializeField] private List<Transform> _pointsPlayerObject;
        [SerializeField] private List<Transform> _pointsEnemy;

        private int _countPlayerObject = -1;
        private int _countEnemyObject = -1;

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

        public void DisableItem() => Destroy(gameObject);
    }
}