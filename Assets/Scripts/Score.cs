using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float moveSpeed;
    private float minY = -7;    // 화면 밖을 벗어나는 기준
    private bool isPractice;

    void Start() {
        isPractice = GameManager.instance.GetIsPractice();

        if (!isPractice) {
            moveSpeed = PlayerPrefs.GetFloat("SpeedLevel");
        }
        else {
            moveSpeed = 5f;
        }

        if (gameObject.tag == "A+" || gameObject.tag == "A0") {
            GameManager.instance.IncreaseGoodSpawnCount();
        }
        else if (gameObject.tag == "B0" || gameObject.tag == "C+") {
            GameManager.instance.IncreaseBadSpawnCount();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPractice) {
            float elapsed = Time.time - GameManager.instance.GetGameStartTime();

             // 20초 단위로 속도 증가
            if (elapsed >= 20f && elapsed < 40f) {
                moveSpeed = 7f;
            }
            else if (elapsed >= 40f) {
                moveSpeed = 9f;
            }
        }
        
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    // 플레이어와 충돌 시 점수 객체는 화면에서 사라짐
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
