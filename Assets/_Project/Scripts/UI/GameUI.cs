using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.Services.PauseSystem;
using _Project.Scripts.UI.HPBar;
using _Project.Scripts.UI.SaveAndLoadUI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealtBar _hpBar;
        [SerializeField] private SaveAndLoadView _saveView;

        private PlayerItem _subscribedItem;
        private SaveAndLoadView _saveViewInstance;
        private SaveAndLoadModel _saveAndLoadModel;
        private PauseService _pauseService;

        private void OnEnable(){ _pauseService.OnPause += ShowSaveScreen;}

        [Inject]
        public void Construct(SaveAndLoadModel saveAndLoadModel, PauseService pauseService)
        {
            _saveAndLoadModel = saveAndLoadModel;
            _pauseService = pauseService;
        }

        public void SubscribeInObject(PlayerItem playerItem)
        {
            if (_subscribedItem != null)
                UnsubscribeInObject();

            _subscribedItem = playerItem;
            _subscribedItem.OnHealthChanged += _hpBar.SetNewData;
            _hpBar.SetNewData(playerItem.Health);
        }

        public void ShowSaveScreen()
        {
            if (_saveViewInstance != null)
            {
                Destroy(_saveViewInstance.gameObject);
                return;
            }

            _saveViewInstance = Instantiate(_saveView, transform);
            var presenter = new SaveAndLoadPresenter(_saveAndLoadModel);
            _saveAndLoadModel.SetView(_saveViewInstance);
            _saveViewInstance.Initialize(presenter);
        }

        private void UnsubscribeInObject() => _subscribedItem.OnHealthChanged -= _hpBar.SetNewData;

        private void OnDisable(){ _pauseService.OnPause += ShowSaveScreen; }
    }
}