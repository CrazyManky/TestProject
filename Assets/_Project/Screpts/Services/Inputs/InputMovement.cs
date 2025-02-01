using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class InputMovement : IInputMovement
    {
        private Dictionary<KeyCode, Vector3> _keyDirections;

        public Vector3 MoveDirection { get; set; }

        public InputMovement()
        {
            _keyDirections = new Dictionary<KeyCode, Vector3>
            {
                { KeyCode.W, Vector3.forward },
                { KeyCode.S, Vector3.back },
                { KeyCode.A, Vector3.left },
                { KeyCode.D, Vector3.right },
            };
        }

        public void HandleInput()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            MoveDirection = Vector3.zero;
            foreach (var keyDirection in _keyDirections)
            {
                if (Input.GetKey(keyDirection.Key))
                {
                    MoveDirection += keyDirection.Value;
                }
            }

            MoveDirection = MoveDirection.normalized;
        }
    }
}