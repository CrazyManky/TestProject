using _Project.Screpts.Services.LoadSystem.ConfigLoading;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.EnemyComponents
{
    public class BaseEnemy : MonoBehaviour, ISaveData, IPauseItem, IDestroy
    {
        [SerializeField] private string _keyItem;

        public bool PauseActive { get; private set; }
        public IConfigHandler ConfigHandler { get; private set; }
        public string Key => _keyItem;


        [Inject]
        public void Construct(IConfigHandler configHandler) => ConfigHandler = configHandler;

        public void SetPosition(Vector3 position) => transform.position = position;

        public void Load(ISaveData data)
        {
            // if (data is DataEnemy dataEnemy)
            // {
            //     gameObject.SetActive(dataEnemy.IsActive);
            //     transform.position = JsonConvert.DeserializeObject<Vector3>(dataEnemy.Position);
            // }
        }

        public object SaveData()
        {
            var data = new DataEnemy(_keyItem, gameObject.activeSelf, transform.position);
            return data;
        }

        public void Pause() => PauseActive = true;
        public void Continue() => PauseActive = false;
        public void DisableItem() => Destroy(gameObject);
    }
}