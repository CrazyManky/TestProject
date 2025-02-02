using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.SaveSystem;
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

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector, GameItemsConteiner<MoveObject> playerObjectConteiner,
            SaveService saveService, LoadingService loadingService, HandlerLose handlerLose)
        {
            _playerObjectConteiner = playerObjectConteiner;
            _playerObjectCollector = playerObjectCollector;
            _saveService = saveService;
            _loadingService = loadingService;
            _handlerLose = handlerLose;
        }


        public MoveObject CreateMoveableObject(int itemIndex)
        {
            var instance = Object.Instantiate(_playerObjectConteiner.GetObject(itemIndex));
            _playerObjectCollector.AddMovebleObject(instance);
            _saveService.AddSaveItem(instance);
            _loadingService.AddLoadingItem(instance);
            _handlerLose.Subscribe(instance);
            return instance;
        }
    }
}