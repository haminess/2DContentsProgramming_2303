using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    // ������ ���� ���� ��ũ��Ʈ
    // ������ ������Ʈ
    public Animator animator;    
    
    // ������ ��ġ
    public Enemy monkey;      // �ڱ� ������Ʈ
    public GameObject banana; // ������ ����
    public int attackTime;    // �ٳ��� �Ѿ� ���� �ð�
    public int attackPower;   // �ٳ��� �Ѿ� ��

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        animator = GetComponent<Animator>();
        monkey = GetComponent<Enemy>();

        // �ٳ��� ���� ������ �����ϴ� �Լ�
        Think();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // ���� �ൿ ���� ����
    // ����Լ�
    void Think()
    {
        // ���� ���� �ð� ���� �Ҵ�
        attackTime = Random.Range(1, 5);

        // ���� ������ �ð� ���� �Ҵ�
        float nextThinkTime = Random.Range(2f, 5f);

        // ������ ���� �޼��� ����, ���� �Լ� ����
        // Invoke �Լ��� string �Լ��� int �ð� �Ŀ� ȣ�����ش�
        Invoke("MonkeyAttack", attackTime);
        Invoke("Think", nextThinkTime);

    }

    void MonkeyAttack()
    {
        // ������ �ٳ��� ����
        monkey.animator.SetTrigger("Attack");                        // ���� �ִϸ��̼� On
        Vector2 bulletPos = transform.position;                      // �Ѿ� ��ġ ����
        Quaternion bulletquater = transform.rotation;                // �Ѿ� ȸ�� ����
        Bullet bulletController = banana.GetComponent<Bullet>();     // �Ѿ� ������Ʈ �ҷ�����
        bulletController.direction = monkey.spriteRenderer.flipX;    // �Ѿ� ���� ����
        bulletController.bulletPower = attackPower;                  // �Ѿ� �� ����
        Instantiate(banana, bulletPos, bulletquater);                // �Ѿ� ����, ���
    }

}
