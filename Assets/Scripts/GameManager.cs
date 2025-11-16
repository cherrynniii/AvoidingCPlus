using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  // GameManager 총괄 instance(싱글톤 패턴)
    private int finalScore = 0;     // 누적 점수

    [SerializeField] private TextMeshProUGUI totalScoreText; // 화면에 보이는 점수 텍스트
    [SerializeField] private TextMeshProUGUI playerNameText; //화면에 보이는 이름
    [SerializeField] private GameObject playerObject; //플레이어 오브젝트
    

    private bool gameStarted = false;
    private float gameStartTime = 0f;   // 첫 score 생성 후 시간이 얼마나 지났는지

    [SerializeField]
    private GameObject gameOverPanel;

    private int goodSpawnCount = 0;     // A+, A0 생성 횟수
    private int badSpawnCount = 0;      // B0, C+ 생성 횟수
    private int goodCollectedCount = 0; // A+, A0 받은 횟수
    private int badCollectedCount = 0;  // B0, C+ 받은 횟수
    private string participantID = "";
    private float speed = 0f;
    private float spawnInterval = 0f;

    // 게임 실행 시 맨 처음 실행 (GameManager 인스턴스 초기화)
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //TitleScene에서 저장된 이름 불러오기
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

        if (playerNameText != null)
            playerNameText.SetText(playerName);

        ApplyPlayerSize();
    }

    private void ApplyPlayerSize()
    {
        int level = PlayerPrefs.GetInt("PlayerSize", 1);
        float scale = 0.5f + (level - 1) * 0.125f;

        if (playerObject != null)
            playerObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    // 누적 점수 (finalScore) 값을 value 만큼 증가 시키기
    public void IncreaseFinalScore(int value) {
        finalScore += value;
        totalScoreText.SetText(finalScore.ToString());
    }

    // FinalScore 값 보기 (디버깅 시 사용하였음)
    public int GetFinalScore() {
        return finalScore;
    }
    
    // 게임 종료 처리
    public void SetGameOver() {
        Debug.Log("GAME OVER (40초 지남)");
        ScoreSpawner scoreSpawner = FindObjectOfType<ScoreSpawner>();
        if (scoreSpawner != null) {
            scoreSpawner.StopScoreRoutine();
        }
        Player player = FindObjectOfType<Player>();
        if (player != null) {
            player.DisableMovement();
        }
        SaveResult();
        Invoke("ShowGameOverPanel", 1f);
    }

    // 게임 종료 패널 띄우기
    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    // 게임 시작 초 세기 시작 (첫 score 생성 기준)
    public void NotifyFirstSpawn() {
        if (!gameStarted) {
            gameStartTime = Time.time;
            gameStarted = true;
        }
    }

    void Update() {
        if (gameStarted) {
            // 40초가 지난 경우 게임 종료
            if (Time.time - gameStartTime >= 40f) {
                SetGameOver();
                gameStarted = false;
            }
        }
    }

    // 재시작 처리
    public void PlayAgain() {
        SceneManager.LoadScene("TitleScene");
    }

    // participantID 설정
    public void SetParticipantID(string id) {
        participantID = id;
    }

    // goodSpawnCount 증가
    public void IncreaseGoodSpawnCount() {
        goodSpawnCount += 1;
    }

    // goodCollectedCount 증가
    public void IncreaseGoodCollectedCount() {
        goodCollectedCount += 1;
    }

    // badSpawnCount 증가
    public void IncreaseBadSpawnCount() {
        badSpawnCount += 1;
    }

    // badCollectedCount 증가
    public void IncreaseBadCollectedCount() {
        badCollectedCount += 1;
    }
    
    // 게임 결과 엑셀 파일에 저장하기
    private void SaveResult() {
        spawnInterval = PlayerPrefs.GetFloat("SpawnIntervalLevel");
        speed = PlayerPrefs.GetFloat("SpeedLevel");
        participantID = PlayerPrefs.GetString("StudentID");

        string path = Application.persistentDataPath + "/experiment_log.csv";

        // 파일이 없으면 헤더 생성
        if (!System.IO.File.Exists(path))
        {
            string header = "ParticipantID,Speed,SpawnInterval,FinalScore,GoodSpawnCount,GoodCollectedCount,BadSpawnCount,BadCollectedCount\n";
            System.IO.File.WriteAllText(path, header);
        }

        string line =
            participantID + "," +
            speed + "," +
            spawnInterval + "," +
            finalScore + "," +
            goodSpawnCount + "," +
            goodCollectedCount + "," +
            badSpawnCount + "," +
            badCollectedCount + "\n";

        System.IO.File.AppendAllText(path, line);

        Debug.Log("CSV 저장 완료: " + path);
    }
}
