using _Project._Screpts.GameItems.EnemyComponents;
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
        private GameItemsConteiner<Enemy> _enemyConteiner;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private PauseService _pauseService;

        [Inject]
        public void Construct(GameItemsConteiner<Enemy> enemyConteiner, SaveService saveService, LoadingService loadingService, PauseService pauseService)
        {
            _enemyConteiner = enemyConteiner;
            _saveService = saveService;
            _loadingService = loadingService;
            _pauseService = pauseService;
            Debug.Log(_pauseService);
        }

        public Enemy InstanceEnemy(int itemId)
        {
            var instanceObject = Object.Instantiate(_enemyConteiner.GetObject(itemId));
            _saveService.AddSaveItem(instanceObject);
            _loadingService.AddLoadingItem(instanceObject);
            _pauseService.AddPauseItem(instanceObject);
            return instanceObject;
        }
    }
}