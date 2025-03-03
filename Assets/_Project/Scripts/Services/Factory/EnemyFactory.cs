using _Project.Screpts.Services.Conteiner;
using _Project.Scripts.GameItems.EnemyComponents;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class EnemyFactory
    {
        private GameItemsConteiner<BaseEnemy> _enemyConteiner;
        private SaveDataHandler _saveDataHandler;
        private EntityLoaderService _loadingService;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(GameItemsConteiner<BaseEnemy> enemyConteiner, SaveDataHandler saveDataHandler,
            EntityLoaderService loadingService, IInstantiator instantiator)
        {
            _enemyConteiner = enemyConteiner;
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _instantiator = instantiator;
        }

        public BaseEnemy InstanceEnemy(int itemId)
        {
            var instanceObject =
                _instantiator.InstantiatePrefabForComponent<BaseEnemy>(_enemyConteiner.GetObject(itemId));
            var gameObject = new GameObject(instanceObject.Key);
            _saveDataHandler.AddItem(instanceObject);
            _loadingService.AddLoadingEntity(instanceObject);
            instanceObject.transform.SetParent(gameObject.transform);
            return instanceObject;
        }
    }
}