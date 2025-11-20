using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField studentID;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button practiceButton;

    void Start()
    {
        settingButton.onClick.AddListener(OnSettingClicked);    // 시작 버튼 누르면 이름 저장
        practiceButton.onClick.AddListener(OnPracticeClicked);

        settingButton.interactable = false;
        practiceButton.interactable = true;     // 항상 활성화

        studentID.onValueChanged.AddListener(delegate { CheckInfoInput(); });
    }
    
    void CheckInfoInput()
    {
        string id = studentID.text;
        
        // 이름, 학번 입력 안하면 버튼 비활성화
        settingButton.interactable = !string.IsNullOrEmpty(id);
    }

    void OnSettingClicked()
    {
        string id = studentID.text;

        if (string.IsNullOrEmpty(id))
        {
            Debug.Log("번호를 제대로 입력해주세요!");
            return;
        }

        PlayerPrefs.SetString("StudentID", id);
        PlayerPrefs.SetInt("GameMode", 0);  // 0 = 실전

        SceneManager.LoadScene("SettingScene"); // 실제 게임 씬 이름
    }

    void OnPracticeClicked() {
        PlayerPrefs.SetInt("GameMode", 1);  // 1 = 연습
        SceneManager.LoadScene("SampleScene"); 
    }
}
