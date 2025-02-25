using _Project._Screpts.Interfaces;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.Services.PauseSystem;
using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.GameItems.EnemyComponents
{
    public class EnemyObject : MonoBehaviour, ISaveAndLoad, IPausable, IDestroyGameElement
    {
        [SerializeField] private string _keyItem;

        public string KeyItem => _keyItem;
        public bool PauseAcitve { get; private set; }
        public IConfigHandler ConfigHandler { get; private set; }

        [Inject]
        public void Construct(IConfigHandler configHandler) => ConfigHandler = configHandler;

        public void SetPosition(Vector3 position) => transform.position = position;

        public void Load(ISavableData data)
        {
            if (data is DataEnemy dataEnemy)
            {
                gameObject.SetActive(dataEnemy.IsActive);
                transform.position = new Vector3(dataEnemy.Position.X, dataEnemy.Position.Y, dataEnemy.Position.Z);
            }
        }

        public ISavableData SaveData()
        {
            var data = new DataEnemy(_keyItem, gameObject.activeSelf, transform.position);
            return data;
        }

        public void Pause() => PauseAcitve = true;
        public void Continue() => PauseAcitve = false;
        public void DisableItem() => Destroy(gameObject);
    }
}