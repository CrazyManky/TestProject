using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Project.Scripts.GameItems;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    public class SaveDataHandler : IGameSave
    {
        private List<ISaveData> _saveElements = new();
        private Dictionary<string, object> _saveData = new();
        private const string SaveFileName = "saveData.json";

        public void AddSaveItem(ISaveData data)
        {
            _saveElements.Add(data);
        }

        public async void SaveGameAsync()
        {
            await SaveAsync();
        }

        private async UniTask SaveAsync()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            string jsonData = JsonConvert.SerializeObject(_saveData, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, jsonData);
            Debug.Log($"Данные сохранены в файл: {filePath}");
        }
    }
}