using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Screpts.Services.Conteiner
{
    [Serializable]
    public class GameItemsConteiner<T>
    {
       [SerializeField] private List<T> _items;
       
       public T GetObject(int indexItem)
       {
           if (indexItem >= _items.Count)
           {
               throw new NullReferenceException();
           }

           return _items[indexItem];
       }
    }
}