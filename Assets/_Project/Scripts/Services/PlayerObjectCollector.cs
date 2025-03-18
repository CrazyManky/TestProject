using System.Collections.Generic;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Services
{
    public class PlayerObjectCollector
    {
        private int _activeItemIndex = 0;
        private List<PlayerItem> _switchableElements = new();

        public int ObjectCount => _switchableElements.Count;

        public void AddMoveObject(PlayerItem capsule)
        {
            if (capsule == null)
                return;

            _switchableElements.Add(capsule);
        }

        public PlayerItem GetNewMoveObject()
        {
            if (_switchableElements.Count == 0)
                return null;

            for (int i = 0; i < _switchableElements.Count; i++)
            {
                _activeItemIndex = (_activeItemIndex + 1) % _switchableElements.Count;
                if (_switchableElements[_activeItemIndex].IActive)
                    return _switchableElements[_activeItemIndex];
            }

            return null;
        }

        public void RemoveItem(PlayerItem playerItem)
        {
            if (playerItem == null || !_switchableElements.Contains(playerItem))
                return;

            playerItem.DisableItem();
            _switchableElements.Remove(playerItem);
        }

        public void RemoveItems()
        {
            foreach (var item in _switchableElements)
            {
                if (item != null)
                    Object.Destroy(item.gameObject);
            }

            _switchableElements.Clear();
            _activeItemIndex = 0;
        }
    }
}