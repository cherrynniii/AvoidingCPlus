using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] scores; // A+, A0, B+, B0, C+ 객체
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};   // 객체 생성 x 위치

    [SerializeField]
    private float spawnInterval;  // 객체 생성 주기

    // Start is called before the first frame update
    void Start()
    {
        StartScoreRoutine();
    }

    // coroutine
    void StartScoreRoutine() {
        StartCoroutine("ScoreRoutine");
    }

    // 점수 무한 생성 로직
    IEnumerator ScoreRoutine() {
        yield return new WaitForSeconds(1.5f);  // 시작 1.5초 뒤부터 생성

        while (true) {
            float posX = arrPosX[Random.Range(0, arrPosX.Length)];
            int index = Random.Range(0, scores.Length);     // score 객체 랜덤 뽑기
            SpawnScore(posX, index);
            yield return new WaitForSeconds(spawnInterval); // 생성 주기에 따라 새로운 객체 생성
        }
    }

    // 스폰 x 위치, 어떤 score 객체 만들지 인덱스
    void SpawnScore(float posX, int index) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(scores[index], spawnPos, Quaternion.identity);
    }
}
