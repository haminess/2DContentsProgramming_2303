using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Ư�� ������ �浹���� �� ī�޶� ���� ��ũ��Ʈ
    public GameObject camera1;      // ī�޶�1
    public GameObject camera2;      // ī�޶�2
    public GameObject boss;         // ����
    public GameObject bossHP;       // ����hpUI

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
            // �÷��̾ �� ������ ������

            // ī�޶�1���� 2�� ����
            // ����HP UI Ȱ��ȭ, ������ Ȱ��ȭ
            camera1.SetActive(false);
            camera2.SetActive(true);
            bossHP.SetActive(true);
            boss.SetActive(true);
        }
    }
}
