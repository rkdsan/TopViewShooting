using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider _sliderGauge;
    [SerializeField] private TextMeshProUGUI _currentText;

    public void UpdateBar(int current, int max)
    {
        _sliderGauge.value = (float)current / max;
        _currentText.text = $"{current}";
    }
}
