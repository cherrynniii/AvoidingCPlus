using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] scores; // A+, A0, B+, B0, C+ 객체
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};   // 객체 생성 x 위치
    private int[] weights = {15, 15, 10, 10, 50};    // score 생성 확률 테이블

    private float spawnInterval;  // 객체 생성 주기

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = PlayerPrefs.GetFloat("SpawnIntervalLevel");
        StartScoreRoutine();
    }

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

        int cumulative = 0; // 누적 확률에 따라 어떤 인덱스인지 판단
        for (int i = 0; i < weights.Length; i++) {
            cumulative += weights[i];
            if (rand < cumulative)
                return i;
        }

        return weights.Length - 1;  // 실제로는 실행되지 않음
    }
}
