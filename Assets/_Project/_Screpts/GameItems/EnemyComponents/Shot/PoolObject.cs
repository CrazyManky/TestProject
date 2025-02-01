using System.Collections.Generic;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.Shot
{
    public class PoolObject<T> where T : MonoBehaviour
    {
        private readonly List<T> _pool = new();
        private int _initializedCount = 10;
        private T _prefab;

        public void Initialize(T prefab, Transform transform)
        {
            _prefab = prefab;

            for (int i = 0; i < _initializedCount; i++)
            {
                var instance = Object.Instantiate(prefab, transform);
                instance.gameObject.SetActive(false);
                _pool.Add(instance);
            }
        }

        public T GetItem()
        {
            foreach (var item in _pool)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    item.gameObject.SetActive(true);
                    return item;
                }
            }

            var newInstance = Object.Instantiate(_prefab);
            _pool.Add(newInstance);
            return newInstance;
        }

        public void ReturnItem(T item)
        {
            item.gameObject.SetActive(false);
        }
    }
}