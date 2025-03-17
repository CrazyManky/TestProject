using System;
using System.Collections.Generic;
using _Project.Scripts.Services.PauseSystem;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Inputs
{
    public class InputHandler : ITickable
    {
        private Dictionary<KeyCode, Action> _keyActions;
        private PauseService _pauseService;
        private SwitchingService _switchingService;
        public Vector3 MoveDirection { get; private set; }

        public event Action OnEscapeButtonClick;

        [Inject]
        public void Construct(PauseService pauseService, SwitchingService switchingService)
        {
            _pauseService = pauseService;
            _switchingService = switchingService;
        }

        public void Initialize()
        {
            _keyActions = new Dictionary<KeyCode, Action>()
            {
                [KeyCode.Tab] = _switchingService.SwitchObject,
                [KeyCode.Escape] = () => OnEscapeButtonClick?.Invoke()
            };
        }

        public void Tick()
        {
            HandleActionKey();
            if (_pauseService.Pause)
                return;

            var direaction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDirection = direaction;
        }

        private void HandleActionKey()
        {
            if (_keyActions is null)
                return;

            foreach (var keyAction in _keyActions)
            {
                if (Input.GetKeyDown(keyAction.Key))
                {
                    keyAction.Value.Invoke();
                    break;
                }
            }
        }

        public void Disable()
        {
            _keyActions.Clear();
        }
    }
}