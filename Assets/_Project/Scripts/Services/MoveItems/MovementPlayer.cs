using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.Inputs;
using _Project.Scripts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.MoveItems
{
    public class MovementPlayer : IFixedTickable
    {
        private InputHandler _inputHandler;
        private PlayerItem _activeItem;

        [Inject]
        public void Construct(InputHandler inputHandler, PauseService pauseService)
        {
            _inputHandler = inputHandler;
        }

        public void SetMovementItem(PlayerItem capsule)
        {
            _activeItem = capsule;
        }

        public void FixedTick()
        {
            if (_activeItem == null)
                return;
            _activeItem.Move(_inputHandler.MoveDirection);
        }
    }
}