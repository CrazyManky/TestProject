using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.LoadSystem
{
    public class LoadingService : IDataProvider
    {
        private Dictionary<string, object> _loadedData = new();

        public async UniTask LoadFromFileAsync()
        {
            var saveDirectory = Application.persistentDataPath;
            Debug.Log(saveDirectory);
            var saveFiles = Directory.GetFiles(saveDirectory, "saveData*.json")
                .OrderByDescending(File.GetCreationTime)
                .ToList();

            if (saveFiles.Count == 0)
            {
                Debug.LogWarning("Сохранения не найдены.");
                return;
            }

            var latestSaveFile = saveFiles.First();
            Debug.Log($"Найдено сохранение: {latestSaveFile}");
            var jsonData = await ReadFileAsync(latestSaveFile);
            _loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
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

            var jsonData = await ReadFileAsync(filePath);
            _loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
            Debug.Log($"Данные успешно загружены из файла: {filePath}");
        }

        private async UniTask<string> ReadFileAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            return await reader.ReadToEndAsync();
        }

        public List<string> GetSaveFiles()
        {
            string saveDirectory = Application.persistentDataPath;
            var saveFiles = Directory.GetFiles(saveDirectory, "saveData*.json").OrderBy(File.GetCreationTime).ToList();
            return saveFiles;
        }
        
        public T GetData<T>(string key)
        {
            var jsonData = _loadedData[key].ToString();
            Debug.Log(jsonData);
            var value = JsonConvert.DeserializeObject<T>(jsonData);
            Debug.Log(value);
            return value;
        }
    }

    public interface IDataProvider
    {
        public T GetData<T>(string key);
    }
}