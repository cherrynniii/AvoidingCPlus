using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);    // 시작 버튼 누르면 이름 저장

        startButton.interactable = false;

        nameInput.onValueChanged.AddListener(delegate { CheckNameInput(); });
    }
    
    void CheckNameInput()
    {
        // 이름 입력 안하면 버튼 비활성화
        startButton.interactable = !string.IsNullOrEmpty(nameInput.text);
    }

    void OnStartClicked()
    {
        string playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("이름을 입력해주세요!");
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName); // 이름 저장
        SceneManager.LoadScene("SampleScene"); // 실제 게임 씬 이름
    }
}
