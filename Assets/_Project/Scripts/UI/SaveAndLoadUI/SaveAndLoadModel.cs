using System.Collections.Generic;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem;
using _Project.Screpts.UI.SaveAndLoadUI;
using _Project.Scripts.Services.SaveSystem;
using Zenject;

namespace _Project.Scripts.UI.SaveAndLoadUI
{
    public class SaveAndLoadModel
    {
        private SaveAndLoadView _view;
        private SaveDataHandler _saveDataHandler;
        private LoadingService _loadingService;
        private IBuyStoreItem _buyStoreItem;

        [Inject]
        public void Constructor(SaveDataHandler saveDataHandler, LoadingService loadingService, IBuyStoreItem buyStoreItem)
        {
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _buyStoreItem = buyStoreItem;
        }

        public void SetView(SaveAndLoadView view)
        {
            _view = view;
        }

        public void SaveGame() => _saveDataHandler.SaveGameAsync();


        public void LoadSaveFiles()
        {
            List<string> saveFiles = _loadingService.GetSaveFiles();
            _view.ShowSaveFiles(saveFiles);
        }

        public void BuyStoreItem()
        {
            _buyStoreItem.BuyNoAds();
        }

        public async void LoadSpecificSave(string fileName)
        {
            await _loadingService.LoadFromSpecificFileAsync(fileName);
            //_loadingService.LoadingData();
        }
    }
}