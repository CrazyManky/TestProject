using _Project._Screpts.Interfaces;
using UnityEngine;

namespace _Project._Screpts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(Screpts.GameItems.EnemyComponents.Enemy))]
    public class PathFollowingEnemy : MonoBehaviour
    {
        [SerializeField] private float _patrolDistance;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private Vector3 _startPosition;
        private Vector3 _direction = Vector3.forward;
        private Screpts.GameItems.EnemyComponents.Enemy _enemy;

        private void Awake() => _enemy = GetComponent<Screpts.GameItems.EnemyComponents.Enemy>();


        private void Start()
        {
            _startPosition = transform.position;
        }

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
            if (other.TryGetComponent<IDamageProvaider>(out var damageProvaider))
            {
                damageProvaider.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}