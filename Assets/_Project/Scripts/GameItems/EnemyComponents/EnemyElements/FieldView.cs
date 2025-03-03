using System;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using UnityEngine;

namespace _Project.Scripts.GameItems.EnemyComponents.EnemyElements
{
    public class FieldView : MonoBehaviour
    {
        public event Action<Transform> OnDetectedObject;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerItem moveObject))
            {
                OnDetectedObject?.Invoke(moveObject.transform);
                gameObject.SetActive(false);
            }
        }
    }
}