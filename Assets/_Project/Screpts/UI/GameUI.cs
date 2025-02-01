using _Project._Screpts.GameItems.PlayerObjects.MoveItems;
using _Project._Screpts.LoadSystem;
using _Project._Screpts.SaveSystem;
using _Project._Screpts.UI;
using _Project._Screpts.UI.HPBar;
using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealtBar _hpBar;
        [SerializeField] private SaveAndLoadScreen _saveScreen;

        private MoveObject _subscribedObject;
        private SaveAndLoadScreen _saveScreenInstance;
        private SaveService _saveService;
        private LoadingService _loadingService;

        [Inject]
        public void Construct(SaveService saveService, LoadingService loadingService)
        {
            _saveService = saveService;
            _loadingService = loadingService;
        }

        public void SubscribeInObject(MoveObject moveObject)
        {
            if (_subscribedObject != null)
                UnsubscribeInObject();

            _subscribedObject = moveObject;
            _subscribedObject.OnValueChanged += _hpBar.SetNewData;
            _hpBar.SetNewData(moveObject.Health, moveObject.MaxHealth);
        }

        public void ShowSaveScreen()
        {
            if (_saveScreenInstance != null)
            {
                Destroy(_saveScreenInstance.gameObject);
                return;
            }

            _saveScreenInstance = Instantiate(_saveScreen, transform);
            _saveScreenInstance.Constructor(_saveService, _loadingService);
        }

        private void UnsubscribeInObject() =>
            _subscribedObject.OnValueChanged -= _hpBar.SetNewData;
    }
}