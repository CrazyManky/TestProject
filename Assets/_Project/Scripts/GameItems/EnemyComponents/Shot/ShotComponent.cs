using System;
using System.Collections;
using _Project._Screpts.GameItems.Enemy.Shot;
using _Project.Scripts.MathUtils;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.EnemyComponents.Shot
{
    [RequireComponent(typeof(BaseEnemy))]
    public class ShotComponent : MonoBehaviour
    {
        [SerializeField] private string _keyConfig;
        [SerializeField] private ShootingZone shootingZone;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _firePoint;

        private PoolObject<Projectile> _projectilePool;
        private Transform _target;
        private Coroutine _shootRoutine;
        private BaseEnemy _baseEnemy;
        private IConfigHandler _configHandler;
        private float _turnSpeed;
        private float _shotDelay;
        private float _shootDistance;
        private PauseService _pauseService;


        [Inject]
        public void Construct(IConfigHandler configHandler, PauseService pauseService)
        {
            _configHandler = configHandler;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            _baseEnemy = GetComponent<BaseEnemy>();
            _projectilePool = new PoolObject<Projectile>();
            _projectilePool.Initialize(_projectile, transform);
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _configHandler.GetConfig<ShootingEnemyConfig>(_keyConfig);
            _turnSpeed = config.TurnSpeed;
            _shotDelay = config.ShotDelay;
            _shootDistance = config.ShootDistance;
        }

        private void OnEnable()
        {
            shootingZone.OnEnter += StartShooting;
            shootingZone.OnExited += StopShooting;
            _baseEnemy.OnShot += Shoot;
        }


        private void StartShooting(Transform target)
        {
            _target = target;
            if (_shootRoutine == null)
            {
                _shootRoutine = StartCoroutine(ShotCoroutine());
            }
        }

        private void StopShooting()
        {
            _target = null;
            if (_shootRoutine != null)
            {
                StopCoroutine(_shootRoutine);
                _shootRoutine = null;
            }
        }

        private IEnumerator ShotCoroutine()
        {
            while (_target != null)
            {
                yield return StartCoroutine(RotateTowardsTarget());

                if (MathfDistance.Calculate(transform.position, _target.position) <= _shootDistance)
                    _baseEnemy.ShotToTarget();

                yield return new WaitForSeconds(_shotDelay);
            }
        }

        private IEnumerator RotateTowardsTarget()
        {
            while (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _turnSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, lookRotation) < 5f)
                    break;

                yield return null;
            }
        }

        private void Shoot()
        {
            if (_target == null) return;
            if (_pauseService.Pause) return;

            Projectile newProjectile = _projectilePool.GetItem();
            newProjectile.transform.position = _firePoint.position;
            newProjectile.SetDirection(transform.forward);
            newProjectile.Initialize(_projectilePool);
        }

        private void OnDisable()
        {
            _baseEnemy.OnShot -= Shoot;
            shootingZone.OnEnter -= StartShooting;
            shootingZone.OnExited -= StopShooting;
        }
    }
}