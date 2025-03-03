using _Project.Scripts.GameItems;
using Zenject;

namespace _Project.Scripts.Services.SaveSystem
{
    public class SaveDataHandler : IGameSave
    {
        private ISaveStrategy _saveStrategy;

        [Inject]
        public void Construct(ISaveStrategy saveStrategy) => _saveStrategy = saveStrategy;

        public void AddItem(ISaveData data) => _saveStrategy.AddSaveItem(data);

        public async void SaveGameAsync() => await _saveStrategy.Execute();
    }
}