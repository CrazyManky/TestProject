using UnityEngine;

namespace _Project.Scripts.GameItems.PlayerItems.MoveItems
{
    public class Capsule : PlayerItem
    {
        [SerializeField] private CharacterController _characterController;

        public override void Move(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                return;

            _characterController.Move(moveDirection * PlayerItemData.Speed);
        }

        public override void SetPosition(Vector3 position)
        {
            _characterController.enabled = false;
            transform.position = position;
            _characterController.enabled = true;
        }

        public override void DisableItem()
        {
            base.DisableItem();
            Analytics.NotifyPlayerDead(Key);
        }
    }
}