using System.Collections;
using _Project.Scripts.GameItems;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.Shot
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int _damage = 10;

        private Vector3 _direction;
        private PoolObject<Projectile> _pool;

        public void Initialize(PoolObject<Projectile> pool)
        {
            _pool = pool;
            StartCoroutine(DeactivateAfterTime());
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            transform.position += _direction * (_speed * Time.deltaTime);
        }

        private IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            _pool.ReturnItem(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageProvider>(out var damageProvaider))
            {
                damageProvaider.TakeDamage(_damage);
            }

            _pool.ReturnItem(this);
        }
    }
}