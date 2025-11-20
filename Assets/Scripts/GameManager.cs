using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  // GameManager 총괄 instance(싱글톤 패턴)
    private int finalScore = 0;     // 누적 점수
    private bool isPractice = false;

    [SerializeField] private TextMeshProUGUI totalScoreText; // 화면에 보이는 점수 텍스트
    [SerializeField] private TextMeshProUGUI playerIdText; //화면에 보이는 아이디

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

    private float totalCenterError = 0.01f;    //총 중심 오차 에러
    private int errorSamples = 1;           //에러 샘플 하나 변수

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
        string playerId = PlayerPrefs.GetString("StudentID", "Unknown");
        if (PlayerPrefs.GetInt("GameMode") == 0) {
            isPractice = false;
        }
        else if (PlayerPrefs.GetInt("GameMode") == 1) {
            isPractice = true;
        }

        if (playerIdText != null)
            playerIdText.SetText(playerId);
    }

    // 연습 모드인지 불린 값 반환
    public bool GetIsPractice() {
        return isPractice;
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
        Debug.Log("GAME OVER");
        ScoreSpawner scoreSpawner = FindObjectOfType<ScoreSpawner>();
        if (scoreSpawner != null) {
            scoreSpawner.StopScoreRoutine();
        }
        Player player = FindObjectOfType<Player>();
        if (player != null) {
            player.DisableMovement();
        }
        if (!isPractice) {
            SaveResult();
        }
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
        if (!isPractice) {
            if (gameStarted) {
                // 40초가 지난 경우 게임 종료
                if (Time.time - gameStartTime >= 40f) {
                    SetGameOver();
                    gameStarted = false;
                }
            }
        }
        else {
            if (gameStarted) {
                // 60초가 지난 경우 게임 종료
                if (Time.time - gameStartTime >= 60f) {
                    SetGameOver();
                    gameStarted = false;
                }
            }
        }
    }

    public void RegisterCenterError(float errorRate)
    {
        totalCenterError += errorRate;
        errorSamples++;
    }

    public float GetAverageCenterError()
    {
        if (errorSamples == 0) return 0f;
        return totalCenterError / errorSamples;
    }

public void TempFunction()
{
    float avg = GetAverageCenterError();

    // 퍼센트 변환(+ 100) + 소수점 1자리("F1")
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
        float avgError = GetAverageCenterError();

        string path = Application.persistentDataPath + "/experiment_log.csv";

        // 파일이 없으면 헤더 생성
        if (!System.IO.File.Exists(path))
        {
            string header = "ParticipantID,Speed,SpawnInterval,FinalScore,GoodSpawnCount,GoodCollectedCount,BadSpawnCount,BadCollectedCount,AvgError\n";
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
            badCollectedCount + "," +
            avgError + "\n";

        System.IO.File.AppendAllText(path, line);

        Debug.Log("CSV 저장 완료: " + path);
    }

    // 게임 시작 시간 가져오기
    public float GetGameStartTime() {
        return gameStartTime;
    }
}
