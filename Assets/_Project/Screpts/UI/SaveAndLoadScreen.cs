using System.Collections.Generic;
using _Project._Screpts.LoadSystem;
using _Project._Screpts.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project._Screpts.UI
{
    public class SaveAndLoadScreen : MonoBehaviour
    {
        [SerializeField] private Button _buttonSave;
        [SerializeField] private Button _buttonLoad;
        [SerializeField] private Transform _saveListContainer;
        [SerializeField] private Button _saveButtonPrefab;

        private SaveService _saveService;
        private LoadingService _loadingService;

        private void OnEnable()
        {
            _buttonSave.onClick.AddListener(SaveGame);
            _buttonLoad.onClick.AddListener(ShowSaveFiles);
        }

        public void Constructor(SaveService saveService, LoadingService loadingService)
        {
            _saveService = saveService;
            _loadingService = loadingService;
        }

        public void SaveGame() => _saveService.SaveGameAsync();

        private void ShowSaveFiles()
        {
            foreach (Transform child in _saveListContainer)
            {
                Destroy(child.gameObject);
            }

            List<string> saveFiles = _loadingService.GetSaveFiles();

            foreach (var saveFile in saveFiles)
            {
                var buttonInstance = Instantiate(_saveButtonPrefab, _saveListContainer);
                buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = saveFile;
                buttonInstance.onClick.AddListener(() => LoadSpecificSave(saveFile));
            }
        }

        private async void LoadSpecificSave(string fileName)
        {
            Debug.Log($"Загрузка сохранения: {fileName}");
            await _loadingService.LoadFromSpecificFileAsync(fileName);
            _loadingService.LoadingData();
            Debug.Log("Сохранение загружено!");
        }

        private void OnDisable()
        {
            _buttonSave.onClick.RemoveListener(SaveGame);
            _buttonLoad.onClick.RemoveListener(ShowSaveFiles);
        }
    }
}