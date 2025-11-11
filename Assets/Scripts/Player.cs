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
}
