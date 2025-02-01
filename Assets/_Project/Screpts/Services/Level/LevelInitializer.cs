using System.Collections.Generic;
using _Project._Screpts.GameItems.EnemyComponents;
using _Project._Screpts.GameItems.PlayerObjects;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.Level;
using _Project._Screpts.Services.MoveItems;
using _Project._Screpts.UI;
using _Project.Screpts.GameItems.GameLevel;
using _Project.Screpts.Services.Factory;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Screpts.Services.Level
{
    public class LevelInitializer
    {
        private GameLevel _levelPrefab;
        private FactoryPlayerObjects _factoryPlayerObjects;
        private EnemyFactory _enemyFactory;
        private IInstantiator _instantiator;
        private LevelWinHandle _levelWinHandle;
        private List<MoveObject> _moveObjects = new();
        private List<Enemy> _enemies = new();

        private GameLevel _gameLevelInstance;

        [Inject]
        public void Construct(GameLevel levelPrefab, FactoryPlayerObjects factoryPlayerObjects,
            EnemyFactory enemyFactory, LevelWinHandle levelWinHandle, DiContainer container)
        {
            _levelPrefab = levelPrefab;
            _factoryPlayerObjects = factoryPlayerObjects;
            _enemyFactory = enemyFactory;
            _instantiator = container;
            _levelWinHandle = levelWinHandle;
        }

        public void InitializeLevel(CameraFollow cameraFollow, GameUI gameUI, MovePlayerItems movePlayerItems,
            Transform parent)
        {
            _gameLevelInstance = _instantiator.InstantiatePrefabForComponent<GameLevel>(_levelPrefab, parent);
            _gameLevelInstance.ExitZone.OnEnterObject += _levelWinHandle.CheckWin;
            var moveObject = AddPlayerObjects(_gameLevelInstance);
            AddEnemy(_gameLevelInstance);
            SetItemInSystem(moveObject, cameraFollow, gameUI, movePlayerItems);
        }

        private MoveObject AddPlayerObjects(GameLevel levelInstance)
        {
            var playerInstanceItemOne = _factoryPlayerObjects.CreateMoveableObject(0);
            _moveObjects.Add(playerInstanceItemOne);
            var playerInstanceItemTwo = _factoryPlayerObjects.CreateMoveableObject(1);
            _moveObjects.Add(playerInstanceItemTwo);

            playerInstanceItemOne.SetPosition(levelInstance.GetPlayerPosition());
            playerInstanceItemTwo.SetPosition(levelInstance.GetPlayerPosition());

            return playerInstanceItemOne;
        }

        private void AddEnemy(GameLevel levelInstance)
        {
            var enemyInstanceOne = _enemyFactory.InstanceEnemy(0);
            _enemies.Add(enemyInstanceOne);
            var enemyInstanceTwo = _enemyFactory.InstanceEnemy(1);
            _enemies.Add(enemyInstanceTwo);
            var enemyInstanceFree = _enemyFactory.InstanceEnemy(2);
            _enemies.Add(enemyInstanceFree);

            enemyInstanceOne.SetPosition(levelInstance.GetEnemyPosition());
            enemyInstanceTwo.SetPosition(levelInstance.GetEnemyPosition());
            enemyInstanceFree.SetPosition(levelInstance.GetEnemyPosition());
        }

        private static void SetItemInSystem(MoveObject subItem, CameraFollow cameraFollow, GameUI gameUI,
            MovePlayerItems movePlayerItems)
        {
            gameUI.SubscribeInObject(subItem);
            cameraFollow.SetTarget(subItem);
            movePlayerItems.SetMovementItem(subItem);
        }

        public void DestroyObject()
        {
            _gameLevelInstance.ExitZone.OnEnterObject -= _levelWinHandle.CheckWin;
            ClearCollections();

            _moveObjects = new List<MoveObject>();
            _enemies = new List<Enemy>();
            Object.Destroy(_gameLevelInstance.gameObject);
        }

        private void ClearCollections()
        {
            _moveObjects.ForEach((item) =>
            {
                if (item != null)
                    Object.Destroy(item.gameObject);
            });
            _enemies.ForEach((item) =>
            {
                if (item != null)
                    Object.Destroy(item.gameObject);
            });
        }
    }
}