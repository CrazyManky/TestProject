using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project._Screpts.Interfaces;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Screpts.Services.LoadSystem
{
    public class LoadingService
    {
        private Dictionary<string, ISavableData> _loadedData = new();

        private List<ISaveAndLoad> _loadedSaveAndLoad = new();

        public async UniTask LoadFromFileAsync()
        {
            var saveDirectory = Application.persistentDataPath;
            var saveFiles = Directory.GetFiles(saveDirectory, "saveData_*.json")
                .OrderByDescending(File.GetCreationTime)
                .ToList();

            if (saveFiles.Count == 0)
            {
                Debug.LogWarning("Сохранения не найдены.");
                return;
            }

            var latestSaveFile = saveFiles.First();
            Debug.Log($"Найдено сохранение: {latestSaveFile}");

            try
            {
                var jsonData = await ReadFileAsync(latestSaveFile);
                _loadedData = await UniTask.Run(() =>
                    JsonConvert.DeserializeObject<Dictionary<string, ISavableData>>(jsonData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
                );
            }
            catch (Exception ex)
            {
            }
        }

        public void AddLoadingItem(ISaveAndLoad data)
        {
            _loadedSaveAndLoad.Add(data);
        }

        public async UniTask LoadFromSpecificFileAsync(string fileName)
        {
            var saveDirectory = Application.persistentDataPath;
            var filePath = Path.Combine(saveDirectory, fileName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Файл {filePath} не найден.");
                return;
            }

            try
            {
                var jsonData = await ReadFileAsync(filePath);
                _loadedData = await UniTask.Run(() =>
                    JsonConvert.DeserializeObject<Dictionary<string, ISavableData>>(jsonData, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
                );

                Debug.Log($"Данные успешно загружены из файла: {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private async UniTask<string> ReadFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            return await reader.ReadToEndAsync();
        }

        public List<string> GetSaveFiles()
        {
            string saveDirectory = Application.persistentDataPath;
            var saveFiles = Directory.GetFiles(saveDirectory, "saveData_*.json").OrderBy(File.GetCreationTime).ToList();
            return saveFiles;
        }

        public void ClearLoadingItems()
        {
            _loadedSaveAndLoad.Clear();
        }

        public void LoadingData()
        {
            foreach (var loadingItem in _loadedSaveAndLoad)
            {
                var loadingData = _loadedData[loadingItem.KeyItem];
                loadingItem.Load(loadingData);
            }
        }
    }
}