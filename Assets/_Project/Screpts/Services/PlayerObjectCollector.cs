using System.Collections.Generic;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;

namespace _Project.Screpts.Services
{
    public class PlayerObjectCollector
    {
        private List<MoveObject> _switchableElements = new();
        private int _acitveItemIndx;

        public int ObjectCount => _switchableElements.Count;

        public void AddMovebleObject(MoveObject capsule)
        {
            _switchableElements.Add(capsule);
        }

        public MoveObject GetNewMovebleObject()
        {
            _acitveItemIndx++;
            if (_acitveItemIndx < _switchableElements.Count)
            {
                return _switchableElements[_acitveItemIndx];
            }

            _acitveItemIndx = 0;
            return _switchableElements[_acitveItemIndx];
        }

        public void RemoveItem(MoveObject moveObject)
        {
            _switchableElements.Remove(moveObject);
        }

        public void RemoveItems()
        {
            _switchableElements.ForEach((item) => item.DestroyItem());
            _switchableElements.Clear();
        }
    }
}