using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   // ★ 이거 반드시 있어야 함

public class SettingManager : MonoBehaviour
{
    [SerializeField] private LevelSlider speedSlider;
    [SerializeField] private LevelSlider quantitySlider;
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
    }

    public void OnStartClicked()
    {
        int speed = speedSlider.GetLevel();
        int quantity = quantitySlider.GetLevel();

        PlayerPrefs.SetInt("SpeedLevel", speed);
        PlayerPrefs.SetInt("QuantityLevel", quantity);

        SceneManager.LoadScene("SampleScene");
    }
}
