using _Project._Screpts.GameItems.Enemy;
using _Project._Screpts.GameItems.Enemy.Conteiner;
using _Project._Screpts.GameItems.EnemyComponents;
using _Project._Screpts.LoadSystem;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services.PauseSystem;
using UnityEngine;
using Zenject;

namespace _Project._Screpts.Services.Factory
{
    public class EnemyFactory
    {
        private EnemyConteiner _enemyConteiner;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private PauseService _pauseService;

        [Inject]
        public void Construct(EnemyConteiner enemyConteiner, SaveService saveService, LoadingService loadingService, PauseService pauseService)
        {
            _enemyConteiner = enemyConteiner;
            _saveService = saveService;
            _loadingService = loadingService;
            _pauseService = pauseService;
            Debug.Log(_pauseService);
        }

        public Enemy InstanceEnemy(int itemId)
        {
            var instanceObject = Object.Instantiate(_enemyConteiner.GetItem(itemId));
            _saveService.AddSaveItem(instanceObject);
            _loadingService.AddLoadingItem(instanceObject);
            _pauseService.AddPauseItem(instanceObject);
            return instanceObject;
        }
    }
}