using System.Collections;
using _Project._Screpts.GameItems.Enemy.Shot;
using _Project.Scripts.MathUtils;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.GameItems.EnemyComponents.Shot
{
    [RequireComponent(typeof(BaseEnemy))]
    public class ShotComponent : MonoBehaviour
    {
        [SerializeField] private ShootingZone shootingZone;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _firePoint;

        private float _turnSpeed;
        private float _shotDelay;
        private float _shootDistance;
        private PoolObject<Projectile> _projectilePool;
        private Transform _target;
        private Coroutine _shootRoutine;
        private BaseEnemy _baseEnemy;

        private void Awake()
        {
            _baseEnemy = GetComponent<BaseEnemy>();
            _projectilePool = new PoolObject<Projectile>();
            _projectilePool.Initialize(_projectile, transform);
            LoadingConfig();
        }

        private void LoadingConfig()
        {
            var config = _baseEnemy.ConfigHandler.GetConfig<ShootingEnemyConfig>(_baseEnemy.Key);
            _turnSpeed = config.TurnSpeed;
            _shotDelay = config.ShotDelay;
            _shootDistance = config.ShootDistance;
        }

        private void OnEnable()
        {
            shootingZone.OnEnter += StartShooting;
            shootingZone.OnExited += StopShooting;
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
                    Shoot();

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
            if(_baseEnemy.PauseService.Pause) return;
            
            Projectile newProjectile = _projectilePool.GetItem();
            newProjectile.transform.position = _firePoint.position;
            newProjectile.SetDirection(transform.forward);
            newProjectile.Initialize(_projectilePool);
            _baseEnemy.SoundPlayer.PlayEnemyShotSound(_target != null);
        }

        private void OnDisable()
        {
            shootingZone.OnEnter -= StartShooting;
            shootingZone.OnExited -= StopShooting;
        }
    }
}