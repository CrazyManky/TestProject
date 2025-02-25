using System;
using System.Collections.Generic;
using _Project.Screpts.Services.Inputs;
using _Project.Screpts.UI;
using Services;
using Zenject;

namespace _Project._Screpts.Services.PauseSystem
{
    public class PauseService
    {
        private GameUI _gameUI;
        private InputHandler _inputHandler;
        private bool _isPaused;

        private List<IPausable> _pauseItems = new();


        [Inject]
        public void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void SetUI(GameUI gameUI)
        {
            if (gameUI == null)
                throw new NullReferenceException($"gameUI is null");

            _gameUI = gameUI;
        }


        public void AddPauseItem(IPausable pauseItem)
        {
            _pauseItems.Add(pauseItem);
        }

        public void PauseExecute()
        {
            if (_gameUI != null)
                _gameUI.ShowSaveScreen();

            if (_isPaused)
            {
                PauseDisabled();
                _isPaused = false;
                return;
            }

            _isPaused = true;
            PauseActive();
        }


        private void PauseActive()
        {
            _inputHandler.Pause();
            _pauseItems.ForEach(pauseItem => pauseItem.Pause());
        }

        private void PauseDisabled()
        {
            _inputHandler.Continue();
            _pauseItems.ForEach(pauseItem => pauseItem.Continue());
        }
    }

    public interface IPausable
    {
        void Pause();
        void Continue();
    }
}