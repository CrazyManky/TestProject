using _Project.Scripts.MathUtils;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.EnemyComponents.Movement
{
    [RequireComponent(typeof(BaseEnemy), typeof(Rigidbody))]
    public class PathFollowingEnemy : MonoBehaviour
    {
        [SerializeField] private string _keyConfig;

        private IConfigHandler _configHandler;
        private PauseService _pauseService;
        private Rigidbody _rb;
        private float _patrolDistance;
        private float _speed;
        private int _damage;

        private Vector3 _startPosition;
        private Vector3 _direction = Vector3.forward;

        [Inject]
        public void Construct(IConfigHandler configHandler, PauseService pauseService)
        {
            _configHandler = configHandler;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            LoadingConfig();
        }

        private void Start() => _startPosition = transform.position;

        private void LoadingConfig()
        {
            var config = _configHandler.GetConfig<EnemyPathConfig>(_keyConfig);
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
            if (_pauseService.Pause)
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