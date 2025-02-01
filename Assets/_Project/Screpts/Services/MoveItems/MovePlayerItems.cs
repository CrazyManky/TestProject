using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using Services;
using Zenject;

namespace _Project._Screpts.Services.MoveItems
{
    public class MovePlayerItems
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
            if (_activeItem  == null)
                return;
            
            _activeItem.Move(_inputHandler.InputMovement.MoveDirection);
        }
    }
}