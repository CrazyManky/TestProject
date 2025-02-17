using _Project._Screpts.Interfaces;
using _Project.Screpts.Interfaces;
using UnityEngine;

namespace _Project.Screpts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(EnemyObject))]
    public class PathFollowingEnemy : MonoBehaviour
    {
        private EnemyObject _enemy;
        private float _patrolDistance;
        private float _speed;
        private int _damage;

        private Vector3 _startPosition;
        private Vector3 _direction = Vector3.forward;

        private void Awake()
        {
            _enemy = GetComponent<EnemyObject>();
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _enemy.ConfigHandler.GetConfig(_enemy.KeyItem);
            if (config is EnemyPathConfig configData)
            {
                _patrolDistance = configData.PatrolDistance;
                _speed = configData.Speed;
                _damage = configData.Damage;
            }
        }

        private void Start() => _startPosition = transform.position;


        private void Update()
        {
            if (_enemy.PauseAcitve)
                return;

            MoveAlongPath();
        }


        private void MoveAlongPath()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));

            if (Vector3.Distance(_startPosition, transform.position) >= _patrolDistance)
            {
                _direction = -_direction;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageProvider>(out var damageProvaider))
            {
                damageProvaider.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}