using System.Collections.Generic;
using _Project._Screpts.GameItems.EnemyComponents;
using _Project._Screpts.GameItems.GameLevels;
using _Project._Screpts.GameItems.GameLevels.Levels;
using _Project._Screpts.GameItems.PlayerObjects;
using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.Services.Factory;
using _Project._Screpts.Services.MoveItems;
using _Project._Screpts.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project._Screpts.Services.Level
{
    public class LevelInitializer
    {
        private ConteinerLevels _conteinerLevels;
        private FactoryPlayerObjects _factoryPlayerObjects;
        private EnemyFactory _enemyFactory;
        private DiContainer _container;
        private LevelWinHandle _levelWinHandle;
        private List<MoveObject> _moveObjects = new();
        private List<Enemy> _enemies = new();

        private BaseLevel _levelInstance;

        [Inject]
        public void Construct(ConteinerLevels conteinerLevels, FactoryPlayerObjects factoryPlayerObjects,
            EnemyFactory enemyFactory, LevelWinHandle levelWinHandle, DiContainer container)
        {
            _conteinerLevels = conteinerLevels;
            _factoryPlayerObjects = factoryPlayerObjects;
            _enemyFactory = enemyFactory;
            _container = container;
            _levelWinHandle = levelWinHandle;
        }

        public void InitializeLevel(CameraFollow cameraFollow, GameUI gameUI, MovePlayerItems movePlayerItems)
        {
            _levelInstance = Object.Instantiate(_conteinerLevels.GetLevel());
            _levelInstance.ExitZone.OnEnterObject += _levelWinHandle.CheckWin;
            _container.Inject(_levelInstance);
            _container.Inject(_levelInstance.ExitZone);
            var moveObject = AddPlayerObjects(_levelInstance);
            AddEnemy(_levelInstance);
            SetItemInSystem(moveObject, cameraFollow, gameUI, movePlayerItems);
        }

        private MoveObject AddPlayerObjects(BaseLevel levelInstance)
        {
            var playerInstanceItemOne = _factoryPlayerObjects.CreateMoveableObject(0);
            _moveObjects.Add(playerInstanceItemOne);
            var playerInstanceItemTwo = _factoryPlayerObjects.CreateMoveableObject(1);
            _moveObjects.Add(playerInstanceItemTwo);

            playerInstanceItemOne.SetPosition(levelInstance.GetPlayerPosition());
            playerInstanceItemTwo.SetPosition(levelInstance.GetPlayerPosition());

            return playerInstanceItemOne;
        }

        private void AddEnemy(BaseLevel levelInstance)
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
            _levelInstance.ExitZone.OnEnterObject -= _levelWinHandle.CheckWin;
            ClearCollections();

            _moveObjects = new List<MoveObject>();
            _enemies = new List<Enemy>();
            Object.Destroy(_levelInstance.gameObject);
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