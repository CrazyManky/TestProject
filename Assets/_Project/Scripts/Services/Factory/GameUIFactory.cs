using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class GameUIFactory
    {
        private GameUI _gameUIPrefab;
        private IInstantiator _installer;

        private GameUI _gameUIInstance;

        [Inject]
        public void Construct(GameUI gameUIPrefab, IInstantiator container)
        {
            _gameUIPrefab = gameUIPrefab;
            _installer = container;
        }

        public GameUI InstanceGUI(Transform parent)
        {
            _gameUIInstance = _installer.InstantiatePrefabForComponent<GameUI>(_gameUIPrefab, parent);
            return _gameUIInstance;
        }

        public void DestroyGameUI()
        {
            Object.Destroy(_gameUIInstance.gameObject);
        }
    }
}