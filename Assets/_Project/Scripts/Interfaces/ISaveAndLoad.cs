namespace _Project._Screpts.Interfaces
{
    public interface ISaveAndLoad
    {
        public string KeyItem { get; }
        void Load(ISavableData data);

        ISavableData SaveData();
    }
}