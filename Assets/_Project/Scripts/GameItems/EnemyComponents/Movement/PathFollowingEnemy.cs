using _Project.Scripts.MathUtils;
using UnityEngine;

namespace _Project.Scripts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(BaseEnemy), typeof(Rigidbody))]
    public class PathFollowingEnemy : MonoBehaviour // TODO : Добавить движение через физику
    {
        private BaseEnemy _baseEnemy;
        private Rigidbody _rb;
        private float _patrolDistance;
        private float _speed;
        private int _damage;

        private Vector3 _startPosition;
        private Vector3 _direction = Vector3.forward;

        private void Awake()
        {
            _baseEnemy = GetComponent<BaseEnemy>();
            _rb = GetComponent<Rigidbody>();
            LoadingConfig();
        }

        private void Start() => _startPosition = transform.position;

        private void LoadingConfig()
        {
            var config = _baseEnemy.ConfigHandler.GetConfig<EnemyPathConfig>(_baseEnemy.Key);
            _patrolDistance = config.PatrolDistance;
            _speed = config.Speed;
            _damage = config.Damage;
        }


        private void FixedUpdate()
        {
            MoveAlongPath();
        }


        private void MoveAlongPath()
        {
            if (_baseEnemy.PauseService.Pause)
            {
                _rb.velocity = Vector3.zero;
                return;
            }

            _rb.velocity = _direction * _speed;
            if (MathfDistance.Calculate(_startPosition, transform.position) > _patrolDistance)
            {
                _direction = -_direction;
                _startPosition = transform.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageProvider>(out var damageProvider))
            {
                damageProvider.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}