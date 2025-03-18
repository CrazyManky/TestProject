using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class GameObjectDestroyer
    {
        private List<GameObject> _objects = new();

        public void AddItem(GameObject gameObject)
        {
            _objects.Add(gameObject);
        }

        public void DestroyItems()
        {
            for (int i = 0; i < _objects.Count; i++)
                Object.Destroy(_objects[i].gameObject);

            _objects.Clear();
        }
    }
}