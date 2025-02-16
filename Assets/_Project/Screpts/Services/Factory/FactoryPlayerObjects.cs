﻿using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.Services.Conteiner;
using _Project.Screpts.Services.LoadSystem;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.Services.Factory
{
    public class FactoryPlayerObjects
    {
        private GameItemsConteiner<MoveObject> _playerObjectConteiner;
        private PlayerObjectCollector _playerObjectCollector;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private HandlerLose _handlerLose;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector,
            GameItemsConteiner<MoveObject> playerObjectConteiner,
            SaveService saveService, LoadingService loadingService, HandlerLose handlerLose, IInstantiator instantiator)
        {
            _playerObjectConteiner = playerObjectConteiner;
            _playerObjectCollector = playerObjectCollector;
            _saveService = saveService;
            _loadingService = loadingService;
            _handlerLose = handlerLose;
            _instantiator = instantiator;
        }


        public MoveObject CreateMoveableObject(int itemIndex)
        {
            var gameObject = new GameObject("MoveableObject");
            var instance =
                _instantiator.InstantiatePrefabForComponent<MoveObject>(_playerObjectConteiner.GetObject(itemIndex));
            _playerObjectCollector.AddMovebleObject(instance);
            _saveService.AddSaveItem(instance);
            _loadingService.AddLoadingItem(instance);
            _handlerLose.Subscribe(instance);
            instance.transform.SetParent(gameObject.transform);
            return instance;
        }
    }
}