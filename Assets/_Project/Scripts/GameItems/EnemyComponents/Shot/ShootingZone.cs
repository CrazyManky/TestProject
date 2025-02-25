using System;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using UnityEngine;

namespace _Project.Scripts.GameItems.EnemyComponents.Shot
{
    public class ShootingZone : MonoBehaviour
    {
        public event Action<Transform> OnEnter;
        public event Action OnExited;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerItem moveObject))
            {
                OnEnter?.Invoke(moveObject.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerItem moveObject))
            {
                OnExited?.Invoke();
            }
        }
    }
}