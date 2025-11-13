using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSlider : MonoBehaviour
{
    //슬라이더 변수
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text label;

    private readonly string[] names = { "Low", "Middle", "High" };

    void Start()
    {
        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = 2;

        UpdateLabel((int)slider.value);
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void OnSliderChanged(float val)
    {
        UpdateLabel((int)val);
    }

    private void UpdateLabel(int idx)
    {
        label.text = names[idx];
    }

    public int GetLevel() => (int)slider.value;
}
