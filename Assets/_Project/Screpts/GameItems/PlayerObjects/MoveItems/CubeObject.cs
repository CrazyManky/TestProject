using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;

namespace _Project._Screpts.GameItems.PlayerObjects.MoveItems
{
    public class CubeObject : MoveObject
    {
        [SerializeField] private Rigidbody _rb;

        public override void Move(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                return;

            moveDirection = moveDirection.normalized;
            _rb.velocity = moveDirection * Speed;
        }

        public override void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}