using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.Services.Inputs;
using Zenject;

namespace _Project.Screpts.Services.MoveItems
{
    public class MovementPlayerObjects
    {
        private InputHandler _inputHandler;
        private MoveObject _activeItem;

        [Inject]
        public void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void SetMovementItem(MoveObject capsule)
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