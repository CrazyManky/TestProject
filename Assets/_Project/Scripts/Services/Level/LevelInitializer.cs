﻿using System.Collections.Generic;
using _Project.Scripts.GameItems;
using _Project.Scripts.GameItems.GameLevel;
using _Project.Scripts.GameItems.PlayerItems;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
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

        public void InitializeLevel(CameraFollow cameraFollow, GameUI gameUI, MovementPlayer movementPlayer,
            Transform parent)
        {
            _gameLevelInstance = _instantiator.InstantiatePrefabForComponent<GameLevel>(_levelPrefab, parent);
            _gameLevelInstance.ExitZone.OnEnterObject += _levelWinHandle.CheckWin;
            _destroyGameElements.Add(_gameLevelInstance);
            var moveObject = AddPlayerObjects(_gameLevelInstance);
            AddEnemy(_gameLevelInstance);
            SetItemInSystem(moveObject, cameraFollow, gameUI, movementPlayer);
        }

        private PlayerItem AddPlayerObjects(GameLevel levelInstance)
        {
            var playerInstanceItemOne = _factoryPlayerObjects.CreateMoveableObject(0);
            playerInstanceItemOne.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(playerInstanceItemOne);
            var playerInstanceItemTwo = _factoryPlayerObjects.CreateMoveableObject(1);
            playerInstanceItemTwo.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(playerInstanceItemTwo);

            playerInstanceItemOne.SetPosition(levelInstance.GetPlayerPosition());
            playerInstanceItemTwo.SetPosition(levelInstance.GetPlayerPosition());

            return playerInstanceItemOne;
        }

        private void AddEnemy(GameLevel levelInstance)
        {
            var enemyInstanceOne = _enemyFactory.InstanceEnemy(0);
            enemyInstanceOne.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(enemyInstanceOne);
            var enemyInstanceTwo = _enemyFactory.InstanceEnemy(1);
            enemyInstanceTwo.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(enemyInstanceTwo);
            var enemyInstanceFree = _enemyFactory.InstanceEnemy(2);
            enemyInstanceFree.transform.SetParent(_gameLevelInstance.transform);
            _destroyGameElements.Add(enemyInstanceFree);

            enemyInstanceOne.SetPosition(levelInstance.GetEnemyPosition());
            enemyInstanceTwo.SetPosition(levelInstance.GetEnemyPosition());
            enemyInstanceFree.SetPosition(levelInstance.GetEnemyPosition());
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
            ClearCollections();
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