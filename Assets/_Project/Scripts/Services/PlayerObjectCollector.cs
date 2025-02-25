using System.Collections.Generic;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using Object = UnityEngine.Object;

namespace _Project.Screpts.Services
{
    public class PlayerObjectCollector
    {
        private List<PlayerItem> _switchableElements = new();
        private int _activeItemIndex;

        public int ObjectCount => _switchableElements.Count;

        public void AddMoveObject(PlayerItem capsule)
        {
            _switchableElements.Add(capsule);
        }

        public PlayerItem GetNewMoveObject()
        {
            _activeItemIndex++;
            if (_activeItemIndex <= _switchableElements.Count - 1 && _switchableElements[_activeItemIndex].IActive)
                return _switchableElements[_activeItemIndex];
            _activeItemIndex = 0;
            return _switchableElements[_activeItemIndex];
        }

        public void RemoveItem(PlayerItem playerItem) => _switchableElements.Remove(playerItem);

        public void RemoveItems()
        {
            _switchableElements.ForEach(item => Object.Destroy(item.gameObject));
            _switchableElements.Clear();
        }
    }
}