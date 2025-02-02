using System;
using _Project._Screpts.GameItems.Enemy.EnemyElements;
using _Project._Screpts.Interfaces;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.Movement
{
    [RequireComponent(typeof(Screpts.GameItems.EnemyComponents.Enemy))]
    public class PersecutionObject : MonoBehaviour
    {
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private Screpts.GameItems.EnemyComponents.Enemy _enemy;
        private Transform _target;

        private void OnEnable()
        {
            _fieldView.OnDetectedObject += SetTarget;
        }

        private void Awake()
        {
            _enemy = GetComponent<Screpts.GameItems.EnemyComponents.Enemy>();
        }

        private void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Update()
        {
            if (_enemy.PauseAcitve)
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
            if (other.TryGetComponent(out IDamageProvaider damageProvaider))
            {
                damageProvaider.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _fieldView.OnDetectedObject -= SetTarget;
        }
    }
}