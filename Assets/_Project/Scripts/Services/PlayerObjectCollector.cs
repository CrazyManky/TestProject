using System.Collections.Generic;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using Object = UnityEngine.Object;

namespace _Project.Screpts.Services
{
    public class PlayerObjectCollector
    {
        private List<MoveObject> _switchableElements = new();
        private int _activeItemIndex;

        public int ObjectCount => _switchableElements.Count;

        public void AddMoveObject(MoveObject capsule)
        {
            _switchableElements.Add(capsule);
        }

        public MoveObject GetNewMoveObject()
        {
            _activeItemIndex++;
            if (_activeItemIndex <= _switchableElements.Count - 1 && _switchableElements[_activeItemIndex].IActive)
                return _switchableElements[_activeItemIndex];
            _activeItemIndex = 0;
            return _switchableElements[_activeItemIndex];
        }

        public void RemoveItem(MoveObject moveObject) => _switchableElements.Remove(moveObject);

        public void RemoveItems()
        {
            _switchableElements.ForEach(item => Object.Destroy(item.gameObject));
            _switchableElements.Clear();
        }
    }
}