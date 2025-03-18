using _Project.Screpts.Services.Conteiner;
using _Project.Scripts.GameItems.EnemyComponents;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class EnemyFactory
    {
        private GameItemsConteiner<BaseEnemy> _enemyContainer;
        private SaveDataHandler _saveDataHandler;
        private EntityLoaderService _loadingService;
        private IInstantiator _instantiator;
        private AudioService _audioService;

        [Inject]
        public void Construct(GameItemsConteiner<BaseEnemy> enemyConteiner, SaveDataHandler saveDataHandler,
            EntityLoaderService loadingService, IInstantiator instantiator)
        {
            _enemyContainer = enemyConteiner;
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _instantiator = instantiator;
        }

        public BaseEnemy InstanceEnemy(int itemId, Transform parent)
        {
            var instanceObject =
                _instantiator.InstantiatePrefabForComponent<BaseEnemy>(_enemyContainer.GetObject(itemId));
            _saveDataHandler.AddItem(instanceObject);
            _loadingService.AddLoadingEntity(instanceObject);
            instanceObject.transform.SetParent(parent);
            return instanceObject;
        }
    }
}