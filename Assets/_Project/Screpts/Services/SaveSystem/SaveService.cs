using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project._Screpts.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project._Screpts.SaveSystem
{
    public class SaveService : ISeveData
    {
        private const int MaxSaveFiles = 3;
        private readonly Dictionary<string, ISavableData> _saveData = new();

        public List<ISaveAndLoad> _saveItems = new();

        public void AddSaveItem(ISaveAndLoad saveItem)
        {
            _saveItems.Add(saveItem);
        }

        public async void SaveGameAsync()
        {
            try
            {
                await SaveAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private async UniTask SaveAsync()
        {
            foreach (var saveItem in _saveItems)
            {
                var data = saveItem.SaveData();
                AddData(data.KeyItem, data);
            }

            string saveDirectory = Application.persistentDataPath;
            var saveFiles = Directory.GetFiles(saveDirectory, "saveData_*.json")
                .OrderBy(File.GetCreationTime)
                .ToList();


            if (saveFiles.Count >= MaxSaveFiles)
            {
                File.Delete(saveFiles[0]);
                saveFiles.RemoveAt(0);
            }

            string newSaveFileName = $"saveData_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            string filePath = Path.Combine(saveDirectory, newSaveFileName);

            var jsonData = await UniTask.Run(() =>
                JsonConvert.SerializeObject(_saveData, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            );

            await File.WriteAllTextAsync(filePath, jsonData);

            Debug.Log($"Данные сохранены в файл: {filePath}");
        }

        public void ClearSaveItems()
        {
            _saveItems.Clear();
        }

        private void AddData(string key, ISavableData data)
        {
            _saveData[key] = data;
        }
    }
}