using _Project.Scripts.Services.Inputs;
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
        private InputHandler _inputHandler;

        [Inject]
        public void Construct(GameUI gameUIPrefab, GameOverUI gameOverUI, IInstantiator container,
            InputHandler inputHandler)
        {
            _gameUIPrefab = gameUIPrefab;
            _gameOverUI = gameOverUI;
            _installer = container;
            _inputHandler = inputHandler;
        }

        public GameUI InstanceGUI(Transform parent)
        {
            _gameUIInstance = _installer.InstantiatePrefabForComponent<GameUI>(_gameUIPrefab, parent);
            _inputHandler.OnEscapeButtonClick += _gameUIInstance.ShowSaveScreen;
            return _gameUIInstance;
        }

        public GameOverUI InstanceGameOverScreen()
        {
            var gameObject = new GameObject("GameOverContainer");
            var gameOverUIInstance =
                _installer.InstantiatePrefabForComponent<GameOverUI>(_gameOverUI, gameObject.transform);
            return gameOverUIInstance;
        }

        public GameUI GetInstanceUI()
        {
            if (_gameUIInstance != null)
            {
                return _gameUIInstance;
            }

            return null;
        }

        public void DestroyGameUI()
        {
            _inputHandler.OnEscapeButtonClick -= _gameUIInstance.ShowSaveScreen;
            _gameUIInstance.DisableItem();
        }
    }
}