using System.Collections.Generic;
using _Project._Screpts.SaveSystem;
using _Project.Screpts.Interfaces;
using _Project.Screpts.Services.LoadSystem;
using _Project.Screpts.ShopSystem;
using Zenject;

namespace _Project.Screpts.UI.SaveAndLoadUI
{
    public class SaveAndLoadModel
    {
        private SaveAndLoadView _view;
        private SaveService _saveService;
        private LoadingService _loadingService;
        private IBuyStoreItem _buyStoreItem;

        [Inject]
        public void Constructor(SaveService saveService, LoadingService loadingService, IBuyStoreItem buyStoreItem)
        {
            _saveService = saveService;
            _loadingService = loadingService;
            _buyStoreItem = buyStoreItem;
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

        public void BuyStoreItem()
        {
            _buyStoreItem.BuyNoAds();
        }

        public async void LoadSpecificSave(string fileName)
        {
            await _loadingService.LoadFromSpecificFileAsync(fileName);
            _loadingService.LoadingData();
        }
    }
}