using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // 마우스 커서의 x 위치를 캐릭터의 x 위치로 적용
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);
    }

    // 점수와의 충돌 처리
    private void OnTriggerEnter2D(Collider2D other) {
        float playerX = transform.position.x;
        float scoreX = other.transform.position.x;


        // 중심 거리
        float centerDistance = Mathf.Abs(playerX - scoreX);

        float playerHalf = GetComponent<BoxCollider2D>().bounds.extents.x;
        float scoreHalf = other.GetComponent<BoxCollider2D>().bounds.extents.x;

        // 최대 오차 (끝과 끝이 닿는 지점)
        float maxDistance = playerHalf + scoreHalf;

        // 오차율(0~1)
        float centerErrorRate = centerDistance / maxDistance;
        centerErrorRate = Mathf.Clamp01(centerErrorRate);

        if (other.gameObject.tag == "A+") {
            GameManager.instance.IncreaseFinalScore(3);
            GameManager.instance.IncreaseGoodCollectedCount();
            GameManager.instance.RegisterCenterError(centerErrorRate);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "A0") {
            GameManager.instance.IncreaseFinalScore(1);
            GameManager.instance.IncreaseGoodCollectedCount();
            GameManager.instance.RegisterCenterError(centerErrorRate);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "B+") {
            Debug.Log(GameManager.instance.GetFinalScore());

        }
        else if (other.gameObject.tag == "B0") {
            GameManager.instance.IncreaseFinalScore(-1);
            GameManager.instance.IncreaseBadCollectedCount();
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "C+") {
            GameManager.instance.IncreaseFinalScore(-3);
            GameManager.instance.IncreaseBadCollectedCount();
            Debug.Log(GameManager.instance.GetFinalScore());
        }

    }

    // 게임 종료 시 움직임 비활성화
    public void DisableMovement() {
        this.enabled = false;

        // Rigidbody2D가 있다면 속도 0
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;
    }
}
