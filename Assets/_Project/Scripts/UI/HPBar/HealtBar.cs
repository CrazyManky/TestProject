using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.HPBar
{
    public class HealtBar : MonoBehaviour
    {
        [SerializeField] private Image _viewBar;
        [SerializeField] private TextMeshProUGUI _textValue;

        private int _defaultValue = 100;

        public void SetNewData(int value)
        {
            float fillAmount = (float)value / _defaultValue;
            _viewBar.fillAmount = fillAmount;
            _textValue.text = $"{value} / {_defaultValue}";
        }
    }
}