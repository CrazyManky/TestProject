using System.Collections.Generic;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Services
{
    public class PlayerObjectCollector
    {
        private List<PlayerItem> _switchableElements = new();
        private int _activeItemIndex;

        public int ObjectCount { get; private set; }
        private int _removeItems = 0;

        public void AddMoveObject(PlayerItem capsule)
        {
            ObjectCount++;
            _switchableElements.Add(capsule);
        }

        public PlayerItem GetNewMoveObject()
        {
            _activeItemIndex++;
            if (_activeItemIndex <= ObjectCount  && _switchableElements[_activeItemIndex].IActive)
                return _switchableElements[_activeItemIndex];
            _activeItemIndex = 0;
            return _switchableElements[_activeItemIndex];
        }

        public void RemoveItem(PlayerItem playerItem)
        {
            _removeItems++;
            ObjectCount -= _removeItems;
            playerItem.DisableItem();
        }

        public void RemoveItems()
        {
            foreach (var item in _switchableElements)
            {
                if (item == null)
                    continue;

                Object.Destroy(item.gameObject);
            }

            _switchableElements.Clear();
        }
    }
}