using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.LoadSystem.LoaderEntity;
using _Project.Scripts.Services.SaveSystem;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameItems.EnemyComponents
{
    public class BaseEnemy : MonoBehaviour, ISaveData, ILoadingEntity, IDestroy
    {
        [SerializeField] private string _keyItem;

        private IDataProvider _dataProvider;
        public string Key => _keyItem;


        [Inject]
        public void Construct(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void SetPosition(Vector3 position) => transform.position = position;

        public void Load()
        {
            var data = _dataProvider.GetData<DataEnemy>(Key);
            transform.position = data.Position;
            gameObject.SetActive(data.IsActive);
        }

        public object SaveData()
        {
            var data = new DataEnemy(_keyItem, gameObject.activeSelf, transform.position);
            return data;
        }

        public void DisableItem() => Destroy(gameObject);
    }
}