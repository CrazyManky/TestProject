using _Project.Scripts.GameItems.EnemyComponents.EnemyElements;
using _Project.Scripts.MathUtils;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(BaseEnemy), typeof(Rigidbody))]
    public class PersecutionObject : MonoBehaviour
    {
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private string _keyConfig;

        private Transform _target;
        private Rigidbody _rb;
        private IConfigHandler _configHandler;

        private float _speed;
        private int _damage;
        private float _attackDistance = 1f;
        private PauseService _pauseService;

        [Inject]
        public void Construct(IConfigHandler configHandler, PauseService pauseService)
        {
            _configHandler = configHandler;
            _pauseService = pauseService;
        }

        private void OnEnable() => _fieldView.OnDetectedObject += SetTarget;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _configHandler.GetConfig<EnemyStalkerConfig>(_keyConfig);
            _speed = config.Speed;
            _damage = config.Damage;
        }


        public void FixedUpdate()
        {
            if (_pauseService.Pause)
                return;

            ChasePlayer(_target);
        }

        private void SetTarget(Transform target) => _target = target;

        private void ChasePlayer(Transform target)
        {
            if (!target)
                return;

            Vector3 direction = (_target.position - transform.position).normalized;
            _rb.velocity = direction * _speed;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _rb.MoveRotation(Quaternion.Euler(0, angle, 0));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageProvider damageProvider))
            {
                if (MathfDistance.Calculate(transform.position, _target.position) < _attackDistance)
                {
                    damageProvider.TakeDamage(_damage);
                    gameObject.SetActive(false);
                }
            }
        }

        private void OnDisable() => _fieldView.OnDetectedObject -= SetTarget;
    }
}