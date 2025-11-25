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
        int spawnInterval = quantitySlider.GetLevel();

        float speedValue = ConvertSpeedLevel(speed);
        float spawnIntervalValue = ConvertSpawnIntervalLevel(spawnInterval);

        PlayerPrefs.SetFloat("SpeedLevel", speedValue);
        PlayerPrefs.SetFloat("SpawnIntervalLevel", spawnIntervalValue);

        SceneManager.LoadScene("SampleScene");
    }

    // 스피드 설정
    private float ConvertSpeedLevel(int level)
    {
        switch (level)
        {
            case 0: return 5f;
            case 1: return 7f;
            case 2: return 9f;
            default: return 5f;   // 기본값
        }
    }

    // 생성 주기 설정
    private float ConvertSpawnIntervalLevel(int level)
    {
        switch (level)
        {
            case 0: return 0.8f;
            case 1: return 0.5f;
            case 2: return 0.2f;
            default: return 0.8f;
        }
    }
}
