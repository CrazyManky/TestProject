using _Project._Screpts.Interfaces;
using _Project.Scripts.GameItems;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using UnityEngine;

namespace _Project.Screpts.GameItems.PlayerObjects
{
    public class CameraFollow : MonoBehaviour, IDestroy
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Vector3 _offset;

        private Transform _target;

        public void SetTarget(PlayerItem target)
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

        public void DisableItem()
        {
            Destroy(gameObject);
        }
    }
}