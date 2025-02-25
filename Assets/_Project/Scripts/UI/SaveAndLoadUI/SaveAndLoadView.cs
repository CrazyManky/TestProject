using System.Collections.Generic;
using _Project.Screpts.ShopSystem;
using UnityEngine;
using UnityEngine.UI;


namespace _Project.Screpts.UI.SaveAndLoadUI
{
    public class SaveAndLoadView : MonoBehaviour
    {
        [SerializeField] private Button _buttonSave;
        [SerializeField] private Button _buttonLoad;
        [SerializeField] private Button _byuDisableAdsButton;
        [SerializeField] private Transform _saveListContainer;
        [SerializeField] private SaveDataItem _saveButtonPrefab;

        private SaveAndLoadPresenter _saveAndLoadPresenter;

        public void Initialize(SaveAndLoadPresenter saveAndLoadPresenter)
        {
            _saveAndLoadPresenter = saveAndLoadPresenter;
            _buttonSave.onClick.AddListener(_saveAndLoadPresenter.Save);
            _buttonLoad.onClick.AddListener(_saveAndLoadPresenter.ShowSaveFiles);
            _byuDisableAdsButton.onClick.AddListener(_saveAndLoadPresenter.BuyStoreItem);
            saveAndLoadPresenter.ShowSaveFiles();
        }

        public void ShowSaveFiles(List<string> files)
        {
            foreach (Transform child in _saveListContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var saveFile in files)
            {
                var buttonInstance = Instantiate(_saveButtonPrefab, _saveListContainer);
                buttonInstance.SetPathData(saveFile);
                buttonInstance.ButtonClick.onClick.AddListener(() =>
                    _saveAndLoadPresenter.LoadSaveFiles(buttonInstance.DataPath));
            }
        }

        public void OnDisable()
        {
            _buttonSave.onClick.RemoveListener(_saveAndLoadPresenter.Save);
            _buttonLoad.onClick.RemoveListener(_saveAndLoadPresenter.ShowSaveFiles);
        }
    }
}