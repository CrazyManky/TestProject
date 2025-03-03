using _Project.Screpts.Services.Conteiner;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class FactoryPlayerObjects
    {
        private GameItemsConteiner<PlayerItem> _playerObjectConteiner;
        private PlayerObjectCollector _playerObjectCollector;
        private SaveDataHandler _saveDataHandler;
        private EntityLoaderService _loadingService;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(PlayerObjectCollector playerObjectCollector,
            GameItemsConteiner<PlayerItem> playerObjectConteiner,
            SaveDataHandler saveDataHandler, EntityLoaderService loadingService, IInstantiator instantiator)
        {
            _playerObjectConteiner = playerObjectConteiner;
            _playerObjectCollector = playerObjectCollector;
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _instantiator = instantiator;
        }


        public PlayerItem CreateMoveableObject(int itemIndex)
        {
            var gameObject = new GameObject("MoveableObject");
            var instance =
                _instantiator.InstantiatePrefabForComponent<PlayerItem>(_playerObjectConteiner.GetObject(itemIndex));
            _playerObjectCollector.AddMoveObject(instance);
            _saveDataHandler.AddItem(instance);
            _loadingService.AddLoadingEntity(instance);
            instance.transform.SetParent(gameObject.transform);
            return instance;
        }
    }
}