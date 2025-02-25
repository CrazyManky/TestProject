using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Screpts.UI.HPBar
{
    public class HealtBar : MonoBehaviour
    {
        [SerializeField] private Image _vueBar;
        [SerializeField] private TextMeshProUGUI _textValue;
        

        public void SetNewData(int value, int _maxValue)
        {
            float fillAmount = (float)value / _maxValue;
            _vueBar.fillAmount = fillAmount;
            _textValue.text = $"{value} / {_maxValue}";
        }
    }
}