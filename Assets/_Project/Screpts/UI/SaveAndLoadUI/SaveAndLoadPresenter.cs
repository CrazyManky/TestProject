﻿namespace _Project.Screpts.UI.SaveAndLoadUI
{
    public class SaveAndLoadPresenter
    {
        public SaveAndLoadModel _saveAndLoadModel;

        public SaveAndLoadPresenter(SaveAndLoadModel saveAndLoadModel)
        {
            _saveAndLoadModel = saveAndLoadModel;
        }

        public void ShowSaveFiles()
        {
            _saveAndLoadModel.LoadSaveFiles();
        }

        public void Save() => _saveAndLoadModel.SaveGame();

        public void LoadSaveFiles(string filePath) => _saveAndLoadModel.LoadSpecificSave(filePath);
    }
}