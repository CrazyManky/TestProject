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
        [FormerlySerializedAs("_shotingZone")] [SerializeField] private ShootingZone shootingZone;
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
            var config = _baseEnemy.ConfigHandler.GetConfig(_baseEnemy.Key);
            if (config is ShootingEnemyConfig configData)
            {
                _turnSpeed = configData.TurnSpeed;
                _shotDelay = configData.ShotDelay;
                _shootDistance = configData.ShootDistance;
            }
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
              
                if (MathfDistance.Calculate(transform, _target) <= _shootDistance)
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

            Projectile newProjectile = _projectilePool.GetItem();
            newProjectile.transform.position = _firePoint.position;
            newProjectile.SetDirection(transform.forward);
            newProjectile.Initialize(_projectilePool);
        }

        private void OnDisable()
        {
            shootingZone.OnEnter -= StartShooting;
            shootingZone.OnExited -= StopShooting;
        }
    }
}