using System.Collections.Generic;
using System.IO;
using _Project.Scripts.GameItems;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.SaveSystem
{
    public class SaveToFile : ISaveStrategy
    {
        private List<ISaveData> _items = new();
        private Dictionary<string, object> _saveData = new();
        private const string SaveFileName = "saveData.json";

        public void AddSaveItem<T>(T data) where T : ISaveData
        {
            _items.Add(data);
        }

        public async UniTask Execute()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            _items.ForEach((item) => { _saveData.Add(item.Key, item.SaveData()); });
            string jsonData = JsonConvert.SerializeObject(_saveData, Formatting.Indented, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented});
            await File.WriteAllTextAsync(filePath, jsonData);
            Debug.Log($"Данные сохранены в файл: {filePath}");
        }
    }
}