using UnityEngine;

namespace Services
{
    public interface IInputMovement
    {
        public Vector3 MoveDirection { get; set; }
        
        public void HandleInput();
    }
}