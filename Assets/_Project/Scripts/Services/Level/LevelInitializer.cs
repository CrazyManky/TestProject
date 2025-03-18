using System.Collections.Generic;
using _Project.Scripts.GameItems;
using _Project.Scripts.GameItems.EnemyComponents;
using _Project.Scripts.GameItems.GameLevel;
using _Project.Scripts.GameItems.PlayerItems;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.MoveItems;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Level
{
    public class LevelInitializer
    {
        private GameLevel _levelPrefab;
        private FactoryPlayerObjects _factoryPlayerObjects;
        private EnemyFactory _enemyFactory;
        private IInstantiator _instantiator;
        private LevelWinHandle _levelWinHandle;
        private List<IDestroy> _destroyGameElements = new();
        private AudioService _audioServicePrefab;
        private GameLevel _gameLevelInstance;
        private IPlaySound _audioServiceInstance;

        [Inject]
        public void Construct(GameLevel levelPrefab, AudioService audioService,
            FactoryPlayerObjects factoryPlayerObjects,
            EnemyFactory enemyFactory, LevelWinHandle levelWinHandle, IInstantiator container)
        {
            _levelPrefab = levelPrefab;
            _factoryPlayerObjects = factoryPlayerObjects;
            _enemyFactory = enemyFactory;
            _instantiator = container;
            _levelWinHandle = levelWinHandle;
            _audioServicePrefab = audioService;
        }

        public void InitializeLevel(CameraFollow cameraFollow, GameUI gameUI, MovementPlayer movementPlayer,
            Transform parent)
        {
            _gameLevelInstance = _instantiator.InstantiatePrefabForComponent<GameLevel>(_levelPrefab, parent);
            _audioServiceInstance =
                _instantiator.InstantiatePrefabForComponent<AudioService>(_audioServicePrefab, parent);
            _audioServiceInstance.PlayBackgroundSound();
            _gameLevelInstance.ExitZone.OnEnterObject += _levelWinHandle.CheckWin;
            _gameLevelInstance.ExitZone.OnEnterObject += _audioServiceInstance.PlayCollisionExitZone;
            _destroyGameElements.Add(_gameLevelInstance);
            var moveObject = AddPlayerObjects(parent);
            AddEnemy(parent);
            SetItemInSystem(moveObject, cameraFollow, gameUI, movementPlayer);
        }

        private PlayerItem AddPlayerObjects(Transform parent)
        {
            var playerInstanceItemOne = SettingsPlayerObject(_factoryPlayerObjects.CreateMoveableObject(0, parent));
            SettingsPlayerObject(_factoryPlayerObjects.CreateMoveableObject(1, parent));
            return playerInstanceItemOne;
        }

        private PlayerItem SettingsPlayerObject(PlayerItem playerItem)
        {
            playerItem.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(playerItem);
            playerItem.SetPosition(_gameLevelInstance.GetPlayerPosition());
            return playerItem;
        }

        private void AddEnemy(Transform parent)
        {
            SettingsEnemy(_enemyFactory.InstanceEnemy(0, parent));
            SettingsEnemy(_enemyFactory.InstanceEnemy(1, parent));
            SettingsEnemy(_enemyFactory.InstanceEnemy(2, parent));
        }

        private void SettingsEnemy(BaseEnemy enemy)
        {
            enemy.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(enemy);
            enemy.SetPosition(_gameLevelInstance.GetEnemyPosition());
            enemy.OnShot += _audioServiceInstance.PlayEnemyShotSound;
        }

        private static void SetItemInSystem(PlayerItem subItem, CameraFollow cameraFollow, GameUI gameUI,
            MovementPlayer movementPlayer)
        {
            gameUI.SubscribeInObject(subItem);
            cameraFollow.SetTarget(subItem);
            movementPlayer.SetMovementItem(subItem);
        }

        public void DestroyObject()
        {
            _gameLevelInstance.ExitZone.OnEnterObject -= _levelWinHandle.CheckWin;
            _gameLevelInstance.ExitZone.OnEnterObject -= _audioServiceInstance.PlayCollisionExitZone;
            ClearCollections();
            _audioServiceInstance.PlayBackgroundSound();
            Object.Destroy(_gameLevelInstance.gameObject);
        }

        private void ClearCollections()
        {
            _destroyGameElements.ForEach((item) =>
            {
                if (item != null)
                    item.DisableItem();
            });
            _destroyGameElements.Clear();
        }
    }
}