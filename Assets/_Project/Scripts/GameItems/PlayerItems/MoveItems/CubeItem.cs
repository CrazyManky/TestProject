using UnityEngine;

namespace _Project.Scripts.GameItems.PlayerItems.MoveItems
{
    public class CubeItem : PlayerItem
    {
        [SerializeField] private Rigidbody _rb;

        public override void Move(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                return;

            moveDirection = moveDirection.normalized;
            _rb.velocity = moveDirection * PlayerItemData.Speed;
        }

        public override void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public override void DisableItem()
        {
            base.DisableItem();
            Analytics.NotifyPlayerDead(Key);
            Destroy(gameObject);
        }
    }
}