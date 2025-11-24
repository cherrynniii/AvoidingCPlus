using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] scores; // A+, A0, B+, B0, C+ 객체
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};   // 객체 생성 x 위치
    private int[] weights = {15, 15, 10, 10, 50};    // score 생성 확률 테이블
    
    private bool isPractice;
     private float[] practiceIntervals = { 0.8f, 0.65f, 0.5f };
     private int practiceIndex = 0;

    private float spawnInterval;  // 객체 생성 주기

    // Start is called before the first frame update
    void Start()
    {
        isPractice = GameManager.instance.GetIsPractice();
        if (!isPractice) {
            spawnInterval = PlayerPrefs.GetFloat("SpawnIntervalLevel");
        }
        else {
            spawnInterval = practiceIntervals[0];   // 초기값 0.8f
            StartCoroutine(PracticeIntervalRoutine());
        }
        
        StartScoreRoutine();
    }

    // 오브젝트 사이즈 0.875로 수정 later

    // coroutine
    void StartScoreRoutine() {
        StartCoroutine("ScoreRoutine");
    }

    // 점수 무한 생성 로직
    IEnumerator ScoreRoutine() {
        yield return new WaitForSeconds(1.5f);  // 시작 1.5초 뒤부터 생성
        bool firstSpawn = true; // 첫 생성인지

        while (true) {
            float posX = arrPosX[Random.Range(0, arrPosX.Length)];
            int index = GetWeightedRandomIndex();     // score 객체 랜덤 뽑기
            SpawnScore(posX, index);

            if (firstSpawn) {
                GameManager.instance.NotifyFirstSpawn();
                firstSpawn = false;
            }
            yield return new WaitForSeconds(spawnInterval); // 생성 주기에 따라 새로운 객체 생성
        }
    }

    // 스폰 x 위치, 어떤 score 객체 만들지 인덱스
    void SpawnScore(float posX, int index) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(scores[index], spawnPos, Quaternion.identity);
    }

    public void StopScoreRoutine() {
        StopCoroutine("ScoreRoutine");
    }

    // 확률 기반 랜덤 인덱스 함수
    int GetWeightedRandomIndex() {
        int total = 0;
        foreach (int w in weights)
            total += w; // 100
        
        int rand = Random.Range(0, total);  // 0~99 사이 하나 선택

        if (rand < 50) {
            return 0;
        }
        else {
            return 4;
        }
    }

    // 연습모드: 7초마다 spawnInterval 순환
    IEnumerator PracticeIntervalRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);

            practiceIndex = (practiceIndex + 1) % practiceIntervals.Length;
            spawnInterval = practiceIntervals[practiceIndex];

            Debug.Log("현재 스폰 속도: " + spawnInterval);
        }
    }
}
