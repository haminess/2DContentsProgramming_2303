using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �Ѿ� ��Ʈ�� ��ũ��Ʈ

    public bool direction;             // �Ѿ��� ������ ����
    public int bulletPower;            // �Ѿ� ���ݷ�
    public float bulletSpeed = 0.5f;   // �Ѿ� �ӵ�

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.4f);     // 0.4�� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (!direction)                                                // false�̸�
        {                                                              
            gameObject.transform.Translate(bulletSpeed, 0, 0);         // ����������
        }                                                              
        else                                                           // true�̸�
        {                                                               
            gameObject.transform.Translate(-1 * bulletSpeed, 0, 0);    // �������� ��ǥ �̵� �޼���
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // ���Ϳ� �浹�ϸ�
            collision.gameObject.GetComponent<Enemy>().hp -= bulletPower;                    // ���� ü�� ���
            collision.gameObject.GetComponent<Enemy>().OnEnemyDamaged(transform.position);   // ������ ����Ʈ �߻�
            Destroy(gameObject);                                                             // �ڱ� �Ѿ��� ����
        }
        if(collision.tag == "Player")
        {
            // �÷��̾�� �浹�ϸ�
            collision.gameObject.GetComponent<Player>().OnDamaged(gameObject, bulletPower);  // �÷��̾� ü�� ���
            Destroy(gameObject);                                                             // �Ѿ� ����
        }
    }

}
