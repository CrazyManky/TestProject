using _Project._Screpts.GameItems.PlayerObjects;
using _Project.Screpts.GameItems.PlayerObjects;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.Services.Factory
{
    public class CameraFollowFactory
    {
        private CameraFollow _cameraFollowPrefab;

        private CameraFollow _cameraInstance;

        [Inject]
        public void Construct(CameraFollow cameraFollowPrefab)
        {
            _cameraFollowPrefab = cameraFollowPrefab;
        }

        public CameraFollow InstanceCameraFollow(Transform parent)
        {
            _cameraInstance = Object.Instantiate(_cameraFollowPrefab,parent);
            return _cameraInstance;
        }

        public void DestroyCameraFollow()
        {
            Object.Destroy(_cameraInstance.gameObject);     
        }
    }
}