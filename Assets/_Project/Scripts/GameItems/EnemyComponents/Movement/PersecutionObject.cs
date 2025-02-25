using _Project._Screpts.GameItems.Enemy.EnemyElements;
using _Project.Screpts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(BaseEnemy))]
    public class PersecutionObject : MonoBehaviour
    {
        [SerializeField] private FieldView _fieldView;

        private float _speed;
        private int _damage;

        private BaseEnemy _baseEnemy;
        private Transform _target;

        private void OnEnable() => _fieldView.OnDetectedObject += SetTarget;

        private void Awake()
        {
            _baseEnemy = GetComponent<BaseEnemy>();
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _baseEnemy.ConfigHandler.GetConfig(_baseEnemy.Key);

            if (config is EnemyStalkerConfig configData)
            {
                _speed = configData.Speed;
                _damage = configData.Damage;
            }
        }

        private void SetTarget(Transform target) => _target = target;

        public void Update()
        {
            if (_baseEnemy.PauseActive)
                return;

            ChasePlayer(_target);
        }

        private void ChasePlayer(Transform target)
        {
            if (!target)
                return;

            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * (_speed * Time.deltaTime);
            transform.LookAt(_target.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageProvider damageProvaider))
            {
                damageProvaider.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        private void OnDisable() => _fieldView.OnDetectedObject -= SetTarget;
    }
}