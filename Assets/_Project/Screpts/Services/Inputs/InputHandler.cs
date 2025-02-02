using System;
using System.Collections.Generic;
using _Project._Screpts.Services;
using _Project._Screpts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.Services.Inputs
{
    public class InputHandler : ITickable, IPausable
    {
        private Dictionary<KeyCode, Action> _keyActions;
        private bool _pauseActivated = false;
        private PauseService _pauseService;
        private SwitchingService _switchingService;
        public Vector3 MoveDirection { get; private set; }

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
                [KeyCode.Escape] = _pauseService.PauseExecute,
            };
        }

        public void Tick()
        {
            HandleActionKey();
            if (_pauseActivated)
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

        public void Pause() => _pauseActivated = true;


        public void Continue() => _pauseActivated = false;
    }
}