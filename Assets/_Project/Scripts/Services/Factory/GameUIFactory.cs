using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class GameUIFactory
    {
        private GameUI _gameUIPrefab;
        private GameOverUI _gameOverUI;
        private IInstantiator _installer;
        private GameUI _gameUIInstance;

        [Inject]
        public void Construct(GameUI gameUIPrefab, GameOverUI gameOverUI, IInstantiator container)
        {
            _gameUIPrefab = gameUIPrefab;
            _gameOverUI = gameOverUI;
            _installer = container;
        }

        public GameUI InstanceGUI(Transform parent)
        {
            _gameUIInstance = _installer.InstantiatePrefabForComponent<GameUI>(_gameUIPrefab, parent);
            return _gameUIInstance;
        }

        public GameOverUI InstanceGameOverScreen()
        {
            var gameObject = new GameObject("GameOverContainer");
            var gameOverUIInstance =
                _installer.InstantiatePrefabForComponent<GameOverUI>(_gameOverUI, gameObject.transform);
            return gameOverUIInstance;
        }

        public void DestroyGameUI() => _gameUIInstance.DisableItem();
        
    }
}