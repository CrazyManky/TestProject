using _Project.Scripts.GameItems.PlayerItems;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class CameraFollowFactory
    {
        private CameraFollow _cameraFollowPrefab;
        private IInstantiator _instantiator;
        private CameraFollow _cameraFollowInstance;

        [Inject]
        public void Construct(CameraFollow cameraFollowPrefab, IInstantiator instantiator)
        {
            _cameraFollowPrefab = cameraFollowPrefab;
            _instantiator = instantiator;
        }

        public CameraFollow InstanceCameraFollow(Transform parent)
        {
            _cameraFollowInstance =
                _instantiator.InstantiatePrefabForComponent<CameraFollow>(_cameraFollowPrefab, parent);
            return _cameraFollowInstance;
        }

        public void DestroyInstance() => Object.Destroy(_cameraFollowInstance.gameObject);
    }
}