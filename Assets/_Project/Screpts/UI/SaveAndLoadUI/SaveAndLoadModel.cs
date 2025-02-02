using System.Collections.Generic;
using _Project._Screpts.SaveSystem;
using _Project.Screpts.Services.LoadSystem;
using Zenject;

namespace _Project.Screpts.UI.SaveAndLoadUI
{
    public class SaveAndLoadModel
    {
        private SaveAndLoadView _view;
        private SaveService _saveService;
        private LoadingService _loadingService;

        [Inject]
        public void Constructor(SaveService saveService, LoadingService loadingService)
        {
            _saveService = saveService;
            _loadingService = loadingService;
        }

        public void SetView(SaveAndLoadView view)
        {
            _view = view;
        }

        public void SaveGame() => _saveService.SaveGameAsync();


        public void LoadSaveFiles()
        {
            List<string> saveFiles = _loadingService.GetSaveFiles();
            _view.ShowSaveFiles(saveFiles);
        }

        public async void LoadSpecificSave(string fileName)
        {
            await _loadingService.LoadFromSpecificFileAsync(fileName);
            _loadingService.LoadingData();
        }
    }
}