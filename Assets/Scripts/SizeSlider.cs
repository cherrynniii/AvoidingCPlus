using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizeSlider : MonoBehaviour
{
    //슬라이더 변수
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text label;

    void Start()
    {
        slider.wholeNumbers = true;
        slider.minValue = 1;
        slider.maxValue = 5;

        UpdateLabel((int)slider.value);
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float val)
    {
        UpdateLabel((int)val);
    }

    private void UpdateLabel(int idx)
    {
        label.text = idx.ToString();
    }

    public int GetLevel() => (int)slider.value;
}
