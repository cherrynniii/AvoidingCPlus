using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;  // GameManager 총괄 instance(싱글톤 패턴)
    private int finalScore = 0;     // 누적 점수


    // TODO: isGameOver (boolean)

    [SerializeField] private TextMeshProUGUI totalScoreText; // 화면에 보이는 점수 텍스트
    [SerializeField] private TextMeshProUGUI playerNameText; //화면에 보이는 이름

    // TODO: gameStartPanel (SerializeField): 게임 시작 패널    => 따로 씬 만들어서 구현

    // TODO: gameOverPanel (SerializeField): 게임 종료 패널 (최종 점수 보여주기, Restart 버튼)

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

    // TODO: SetGameStart() 메서드: 게임 시작 패널 띄우기, 사용자 ID 입력(SetPlayerID), SetCondition 호출

    // TODO: SetGameOver() 메서드: 게임 종료 처리, 게임 오버 패널 띄우기(SetGameOverPanel), 게임 결과 저장(saveResult)

    // TODO: SetCondition(int condition) 메서드: SetGameStart()에서 호출됨 -> condition 번호에 따라 낙하 속도 및 생성 주기 설정 
    // (Score 클래스의 SetMoveSpeed와 ScoreSpawner 클래스의 SetSpawnInterval 호출)

    // TODO: SetGameStartPanel() 메서드: 게임 시작 패널 활성화

    // TODO: SetGameOverPanel() 메서드: 게임 종료 패널 활성화

    // TODO: SetPlayerID(int condition) 메서드: 피험자 ID 설정

    // TODO: SaveResult() 메서드: 피험자 ID, 게임 condition, dependent variable 4가지 결과 저장 -> 엑셀 파일

    // TODO: dependent variable 2 계산 메서드 -> A+, A0 받은 비율

    // TODO: dependent variable 3 계산 메서드 -> B0, C+ 받은 비율

    // TODO: dependent variabl 4 계산 메서드 -> 타겟팅 에러

}
