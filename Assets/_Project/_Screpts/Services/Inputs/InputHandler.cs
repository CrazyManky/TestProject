using _Project._Screpts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace Services
{
    public class InputHandler : ITickable, IPausable
    {
        private bool _pauseActivated = false;
        public IInputMovement InputMovement { get; private set; }
        public IInputGamePlayActionHandler GamePlayInput { get; private set; }

        [Inject]
        public void Constructor(IInputMovement inputMovement, IInputGamePlayActionHandler gamePlayInput)
        {
            InputMovement = inputMovement;
            GamePlayInput = gamePlayInput;
        }

        public void Tick()
        {
            GamePlayInput.HandleInput();
            if (_pauseActivated)
                return;
            InputMovement.HandleInput();
        }

        public void Pause() => _pauseActivated = true;


        public void Continue() => _pauseActivated = false;
    }
}