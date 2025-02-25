using _Project.Screpts.Services.Inputs;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using Zenject;

namespace _Project.Scripts.Services.MoveItems
{
    public class MovementPlayer
    {
        private InputHandler _inputHandler;
        private PlayerItem _activeItem;

        [Inject]
        public void Construct(InputHandler inputHandler)
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