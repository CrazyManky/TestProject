using _Project.Screpts.UI.HPBar;
using _Project.Screpts.UI.SaveAndLoadUI;
using _Project.Scripts.GameItems.PlayerItems.MoveItems;
using _Project.Scripts.UI.SaveAndLoadUI;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealtBar _hpBar;
        [SerializeField] private SaveAndLoadView saveView;

        private PlayerItem _subscribedItem;
        private SaveAndLoadView _saveViewInstance;
        private SaveAndLoadModel _saveAndLoadModel;

        [Inject]
        public void Construct(SaveAndLoadModel saveAndLoadModel)
        {
            _saveAndLoadModel = saveAndLoadModel;
        }

        public void SubscribeInObject(PlayerItem playerItem)
        {
            if (_subscribedItem != null)
                UnsubscribeInObject();

            _subscribedItem = playerItem;
            _subscribedItem.OnValueChanged += _hpBar.SetNewData;
            _hpBar.SetNewData(playerItem.Health, playerItem.MaxHealth);
        }

        public void ShowSaveScreen()
        {
            if (_saveViewInstance != null)
            {
                Destroy(_saveViewInstance.gameObject);
                return;
            }

            _saveViewInstance = Instantiate(saveView, transform);
            var prestenter = new SaveAndLoadPresenter(_saveAndLoadModel);
            _saveAndLoadModel.SetView(_saveViewInstance);
            _saveViewInstance.Initialize(prestenter);
        }

        private void UnsubscribeInObject() =>
            _subscribedItem.OnValueChanged -= _hpBar.SetNewData;
    }
}