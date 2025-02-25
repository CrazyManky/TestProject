using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services.PauseSystem;
using _Project.Screpts.GameItems.EnemyComponents;
using _Project.Screpts.Services.Conteiner;
using _Project.Screpts.Services.LoadSystem;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.Services.Factory
{
    public class EnemyFactory
    {
        private GameItemsConteiner<EnemyObject> _enemyConteiner;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private PauseService _pauseService;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(GameItemsConteiner<EnemyObject> enemyConteiner, SaveService saveService,
            LoadingService loadingService, PauseService pauseService, IInstantiator instantiator)
        {
            _enemyConteiner = enemyConteiner;
            _saveService = saveService;
            _loadingService = loadingService;
            _pauseService = pauseService;
            _instantiator = instantiator;
        }

        public EnemyObject InstanceEnemy(int itemId)
        {
            var instanceObject =
                _instantiator.InstantiatePrefabForComponent<EnemyObject>(_enemyConteiner.GetObject(itemId));
            var gameObject = new GameObject(instanceObject.KeyItem);
            _saveService.AddSaveItem(instanceObject);
            _loadingService.AddLoadingItem(instanceObject);
            _pauseService.AddPauseItem(instanceObject);
            instanceObject.transform.SetParent(gameObject.transform);
            return instanceObject;
        }
    }
}