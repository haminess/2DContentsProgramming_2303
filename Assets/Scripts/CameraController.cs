using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 특정 지역에 충돌했을 때 카메라 변경 스크립트
    public GameObject camera1;      // 카메라1
    public GameObject camera2;      // 카메라2
    public GameObject boss;         // 보스
    public GameObject bossHP;       // 보스hpUI

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // 플레이어가 이 지역에 닿으면

            // 카메라1에서 2로 변경
            // 보스HP UI 활성화, 보스도 활성화
            camera1.SetActive(false);
            camera2.SetActive(true);
            bossHP.SetActive(true);
            boss.SetActive(true);
        }
    }
}
