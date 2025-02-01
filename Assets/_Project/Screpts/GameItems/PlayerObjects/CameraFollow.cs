using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.Interfaces;
using UnityEngine;

namespace _Project._Screpts.GameItems.PlayerObjects
{
    public class CameraFollow : MonoBehaviour, IDestroyGameElement
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Vector3 _offset;

        private Transform _target;


        public void SetTarget(MoveObject target)
        {
            _target = target.transform;
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }

            _mainCamera.transform.position = _target.position + _offset;
            _mainCamera.transform.LookAt(_target);
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}