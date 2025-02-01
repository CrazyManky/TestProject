using System;
using System.Collections.Generic;
using _Project._Screpts.Services;
using _Project._Screpts.Services.PauseSystem;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Screpts.Services.Inputs
{
    public class InputHandler : ITickable, IFixedTickable, IPausable
    {
        private Dictionary<KeyCode, Action> _keyActions;
        private bool _pauseActivated = false;
        private PauseService _pauseService;
        private SwitchObjectService _switchObjectService;
        public Vector3 MoveDirection { get; private set; }

        [Inject]
        public void Construct(PauseService pauseService, SwitchObjectService switchObjectService)
        {
            _pauseService = pauseService;
            _switchObjectService = switchObjectService;
        }

        public void Initialize()
        {
            _keyActions = new Dictionary<KeyCode, Action>()
            {
                [KeyCode.Tab] = _switchObjectService.SwitchObject,
                [KeyCode.Escape] = _pauseService.PauseExecute,
            };
        }

        public void Tick()
        {
            if (_pauseActivated)
                return;

            var direaction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            MoveDirection = direaction;
        }

        public void FixedTick()
        {
            HandleActionKey();
        }

        private void HandleActionKey()
        {
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