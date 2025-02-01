using _Project._Screpts.Interfaces;

namespace _Project._Screpts.GameItems.PlayerObjects.MoveItems
{
    public interface ISaveAndLoad
    {
        public void Load(ISavableData loadingData);
        public ISavableData SaveData();
    }
}