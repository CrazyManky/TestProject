using System.Collections.Generic;
using _Project.Scripts.Services.LoadSystem;
using _Project.Scripts.Services.SaveSystem;
using _Project.Scripts.ShopSystem;
using Zenject;

namespace _Project.Scripts.UI.SaveAndLoadUI
{
    public class SaveAndLoadModel
    {
        private SaveAndLoadView _view;
        private SaveDataHandler _saveDataHandler;
        private LoadingService _loadingService;
        private IBuyStoreItem _buyStoreItem;
        private EntityLoaderService _entityLoaderService;

        [Inject]
        public void Constructor(SaveDataHandler saveDataHandler, LoadingService loadingService,
            IBuyStoreItem buyStoreItem, EntityLoaderService entityLoaderService)
        {
            _saveDataHandler = saveDataHandler;
            _loadingService = loadingService;
            _buyStoreItem = buyStoreItem;
            _entityLoaderService = entityLoaderService;
        }

        public void SetView(SaveAndLoadView view) => _view = view;
        
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
            _entityLoaderService.LoadEntities();
        }
    }
}