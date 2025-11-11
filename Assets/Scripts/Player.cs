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
        if (other.gameObject.tag == "A+") {
            GameManager.instance.IncreaseFinalScore(3);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "A0") {
            GameManager.instance.IncreaseFinalScore(1);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "B+") {
            Debug.Log(GameManager.instance.GetFinalScore());

        }
        else if (other.gameObject.tag == "B0") {
            GameManager.instance.IncreaseFinalScore(-1);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
        else if (other.gameObject.tag == "C+") {
            GameManager.instance.IncreaseFinalScore(-3);
            Debug.Log(GameManager.instance.GetFinalScore());
        }
    }
}
