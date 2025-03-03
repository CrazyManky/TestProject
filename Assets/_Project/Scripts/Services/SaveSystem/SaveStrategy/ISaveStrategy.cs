using _Project.Scripts.GameItems;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.SaveSystem
{
    public interface ISaveStrategy
    {
        public void AddSaveItem<T>(T data) where T : ISaveData;
        public UniTask Execute();
    }
}