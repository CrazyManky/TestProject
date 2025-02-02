using System.Collections;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.Shot
{
    [RequireComponent(typeof(Screpts.GameItems.EnemyComponents.Enemy))]
    public class ShotComponent : MonoBehaviour
    {
        [SerializeField] private ShotingZone _shotingZone;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _turnSpeed = 5f;
        [SerializeField] private float _shotDelay = 1.5f;
        [SerializeField] private float _shootDistance = 10f;

        private PoolObject<Projectile> _projectilePool;
        private Transform _target;
        private Coroutine _shootRoutine;

        private void Awake()
        {
            _projectilePool = new PoolObject<Projectile>();
            _projectilePool.Initialize(_projectile, transform);
        }

        private void OnEnable()
        {
            _shotingZone.OnEnterZone += StartShooting;
            _shotingZone.OnExitZone += StopShooting;
        }

        private void OnDisable()
        {
            _shotingZone.OnEnterZone -= StartShooting;
            _shotingZone.OnExitZone -= StopShooting;
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

                if (Vector3.Distance(transform.position, _target.position) <= _shootDistance)
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
    }
}