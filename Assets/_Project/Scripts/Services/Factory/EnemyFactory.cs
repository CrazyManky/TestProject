using _Project._Screpts.Services.PauseSystem;
using _Project.Screpts.Services.Conteiner;
using _Project.Screpts.Services.LoadSystem;
using _Project.Scripts.GameItems.EnemyComponents;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Factory
{
    public class EnemyFactory
    {
        private GameItemsConteiner<BaseEnemy> _enemyConteiner;
        private SaveDataHandler _saveDataHandler;
        private LoadingService _loadingService;
        private PauseService _pauseService;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(GameItemsConteiner<BaseEnemy> enemyConteiner, SaveDataHandler saveDataHandler,
            LoadingService loadingService, PauseService pauseService, IInstantiator instantiator)
        {
            _enemyConteiner = enemyConteiner;
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _pauseService = pauseService;
            _instantiator = instantiator;
        }

        public BaseEnemy InstanceEnemy(int itemId)
        {
            var instanceObject =
                _instantiator.InstantiatePrefabForComponent<BaseEnemy>(_enemyConteiner.GetObject(itemId));
            var gameObject = new GameObject(instanceObject.Key);
            _saveDataHandler.AddSaveItem(instanceObject);
           // _loadingService.AddLoadingItem(instanceObject);
            _pauseService.AddPauseItem(instanceObject);
            instanceObject.transform.SetParent(gameObject.transform);
            return instanceObject;
        }
    }
}