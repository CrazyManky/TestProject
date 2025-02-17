using _Project._Screpts.GameItems.Enemy.EnemyElements;
using _Project._Screpts.Interfaces;
using _Project.Screpts.Interfaces;
using UnityEngine;

namespace _Project.Screpts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(EnemyObject))]
    public class PersecutionObject : MonoBehaviour
    {
        [SerializeField] private FieldView _fieldView;

        private float _speed;
        private int _damage;

        private EnemyObject _enemyObject;
        private Transform _target;

        private void OnEnable() => _fieldView.OnDetectedObject += SetTarget;

        private void Awake()
        {
            _enemyObject = GetComponent<EnemyObject>();
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _enemyObject.ConfigHandler.GetConfig(_enemyObject.KeyItem);

            if (config is EnemyStalkerConfig configData)
            {
                _speed = configData.Speed;
                _damage = configData.Damage;
            }
        }

        private void SetTarget(Transform target) => _target = target;

        public void Update()
        {
            if (_enemyObject.PauseAcitve)
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