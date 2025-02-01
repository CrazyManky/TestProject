using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Screpts.UI.SaveAndLoadUI
{
    public class SaveDataItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _dataPath;

        public Button ButtonClick => _button;

        public string DataPath { get; private set; }

        public void SetPathData(string dataPath)
        {
            DataPath = dataPath;
            _dataPath.text = DataPath;
        }
    }
}