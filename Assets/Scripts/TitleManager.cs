using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField studentID;
    [SerializeField] private Button settingButton;

    void Start()
    {
        settingButton.onClick.AddListener(OnSettingClicked);    // 시작 버튼 누르면 이름 저장

        settingButton.interactable = false;

        nameInput.onValueChanged.AddListener(delegate { CheckInfoInput(); });
        studentID.onValueChanged.AddListener(delegate { CheckInfoInput(); });
    }
    
    void CheckInfoInput()
    {
        string name = nameInput.text;
        string id = studentID.text;
        // 이름, 학번 입력 안하면 버튼 비활성화
        bool nameValid = !string.IsNullOrEmpty(name);
        bool idValid = Regex.IsMatch(id, @"^\d{8}$");

        settingButton.interactable = nameValid && idValid;
    }

    void OnSettingClicked()
    {
        string playerName = nameInput.text;
        string id = studentID.text;

        if (string.IsNullOrEmpty(playerName) || !Regex.IsMatch(id, @"^\d{8}$"))
        {
            Debug.Log("이름 또는 학번을 제대로 입력해주세요!");
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName); // 이름 저장
        PlayerPrefs.SetString("StudentID", id);

        SceneManager.LoadScene("SettingScene"); // 실제 게임 씬 이름
    }
}
