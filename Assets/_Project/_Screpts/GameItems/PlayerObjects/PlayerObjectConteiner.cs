using System;
using System.Collections.Generic;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;

namespace _Project._Screpts.GameItems.PlayerObjects
{
    [Serializable]
    public struct PlayerObjectConteiner
    {
        [SerializeField] private List<MoveObject> _moveableObjects;

        public MoveObject GetMoveableObject(int indexItem)
        {
            if (indexItem >= _moveableObjects.Count)
            {
                throw new NullReferenceException();
            }

            return _moveableObjects[indexItem];
        }
    }
}