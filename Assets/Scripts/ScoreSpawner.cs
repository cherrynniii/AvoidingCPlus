using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] scores; // A+, A0, B+, B0, C+

    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };

    private bool isPractice;
    private float[] practiceIntervals = { 0.8f, 0.65f, 0.5f };
    private int practiceIndex = 0;
    private float spawnInterval;

    // 🔥 셔플백 리스트 (A+ = 0, C+ = 4)
    private List<int> bag = new List<int>();
    public int pairCount = 2;

    void Start()
    {
        // 셔플백 초기 생성
        RefillAndShuffleBag();

        isPractice = GameManager.instance.GetIsPractice();
        if (!isPractice) {
            spawnInterval = PlayerPrefs.GetFloat("SpawnIntervalLevel");
        }
        else {
            spawnInterval = practiceIntervals[0];
            StartCoroutine(PracticeIntervalRoutine());
        }

        StartScoreRoutine();
    }

    void StartScoreRoutine() {
        StartCoroutine("ScoreRoutine");
    }

    IEnumerator ScoreRoutine() {
        yield return new WaitForSeconds(1.5f);
        bool firstSpawn = true;

        while (true) {

            float posX = arrPosX[Random.Range(0, arrPosX.Length)];

            // ⬇️ 기존 랜덤 대신 "셔플백"에서 뽑기
            int index = GetNextFromBag();

            SpawnScore(posX, index);

            if (firstSpawn) {
                GameManager.instance.NotifyFirstSpawn();
                firstSpawn = false;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnScore(float posX, int index) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(scores[index], spawnPos, Quaternion.identity);
    }

    public void StopScoreRoutine() {
        StopCoroutine("ScoreRoutine");
    }

    // -----------------------------------------------------------
    // 🔥 셔플백 방식 구현
    // -----------------------------------------------------------

    // 주머니에서 하나 뽑기
    int GetNextFromBag()
    {
        if (bag.Count == 0)
            RefillAndShuffleBag();

        int next = bag[0];
        bag.RemoveAt(0);
        return next;
    }

    // A+ & C+을 일정 수 넣고 섞기
    void RefillAndShuffleBag()
    {
        bag.Clear();

        for (int i = 0; i < pairCount; i++)
        {
            bag.Add(0); // A+
            bag.Add(4); // C+
        }

        // Fisher–Yates Shuffle
        for (int i = bag.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = bag[i];
            bag[i] = bag[rand];
            bag[rand] = temp;
        }
    }

    // -----------------------------------------------------------

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
