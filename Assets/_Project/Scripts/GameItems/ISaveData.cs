namespace _Project.Scripts.GameItems
{
    public interface ISaveData
    {
        public string Key { get;}
        object SaveData();
    }
}