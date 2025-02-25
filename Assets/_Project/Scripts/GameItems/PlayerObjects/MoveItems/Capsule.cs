using UnityEngine;

namespace _Project.Screpts.GameItems.PlayerObjects.MoveItems
{
    public class Capsule : MoveObject
    {
        [SerializeField] private CharacterController _characterController;

        public override void Move(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                return;

            _characterController.Move(moveDirection * MoveableObjectData.Speed);
        }

        public override void SetPosition(Vector3 position)
        {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }
    }
}