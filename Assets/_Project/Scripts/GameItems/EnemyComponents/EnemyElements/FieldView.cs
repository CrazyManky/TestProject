using System;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;

namespace _Project._Screpts.GameItems.Enemy.EnemyElements
{
    public class FieldView : MonoBehaviour
    {
        public event Action<Transform> OnDetectedObject;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MoveObject moveObject))
            {
                OnDetectedObject?.Invoke(moveObject.transform);
            }
        }
    }
}