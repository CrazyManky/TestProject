using _Project.Screpts.GameItems.PlayerObjects.MoveItems;
using _Project.Screpts.UI.HPBar;
using _Project.Screpts.UI.SaveAndLoadUI;
using UnityEngine;
using Zenject;

namespace _Project.Screpts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealtBar _hpBar;
        [SerializeField] private SaveAndLoadView saveView;

        private MoveObject _subscribedObject;
        private SaveAndLoadView _saveViewInstance;
        private SaveAndLoadModel _saveAndLoadModel;

        [Inject]
        public void Construct(SaveAndLoadModel saveAndLoadModel)
        {
            _saveAndLoadModel = saveAndLoadModel;
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
            _subscribedObject.OnValueChanged -= _hpBar.SetNewData;
    }
}