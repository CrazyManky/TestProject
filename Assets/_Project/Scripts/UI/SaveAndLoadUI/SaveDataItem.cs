using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.SaveAndLoadUI
{
    public class SaveDataItem : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [field:SerializeField] public TextMeshProUGUI _dataPath { get; private set; }

        public Button ButtonClick => _button;

        public string DataPath { get; private set; }

        public void SetPathData(string dataPath)
        {
            DataPath = dataPath;
            _dataPath.text = DataPath;
        }
    }
}