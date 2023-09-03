using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ���� ���� ��ũ��Ʈ
    // ������Ʈ �ҷ�����
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public BoxCollider2D boxCollider;

    // �÷��̾� ������Ʈ �ҷ�����
    public GameObject player;

    // ���� ����
    public int hp;                 // ���� ü��
    public int maxHp;              // �ִ� ü��
    public int power;              // ����� �� ��
    public int attackPower;        // ���ݷ�
    public int speed;              // �̵� �ӵ�
    public bool isDie = false;     // �׾����� ����

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        player = GameObject.Find("Bunny");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        // ������ �� ü�� ä���ֱ�
        hp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ʾҴµ� ü���� 0 ���ϰ� �Ǹ�
        if(hp <= 0 && !isDie)
        {
            // ����
            OnDie();
            return;
        }

        // �׾ �۵� ����
        if (isDie)
        {
            return;
        }

        // �̵�
        Move();
        
    }

    void Move()
    {
        // ���� �̵� �޼���
        // �÷��̾� ��Ȱ��ȭ �Ǿ������� ����
        if (!player)    
        {
            return;
        }

        // �÷��̾�� ���� ���� �Ÿ� ���
        Vector2 distance = player.transform.position - transform.position;
        // �÷��̾ ���ʿ� ������ -1, ������ 1
        int head = distance.x > 0 ? 1 : -1;


        // �ٴڰ� ������ �̵�, �ٴ��� ������ ����
        Vector2 frontVec = new Vector2(rigid.position.x + head * 1f, rigid.position.y);
        // ���� �������� ���̸� �����Ͽ� ���̿� �浹�� �÷����� �ִ��� üũ�ϰ� �޾ƿ���
        // Raycast�� Ray�� ������Ű�� RaycastHit2D�� ��ȯ, RaycastHit2D�� �浹�� ��ü�� �������� ����
        // ���� ��ġ���� 1 �տ���, ���� �Ʒ� ��������, ���� 10��ŭ, Platform ���̾ ����
        // ��ü �浹 ������Ʈ ��ȯ
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 10, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)     // �տ� �浹�� �÷����� ������
        {                                 
            return;                      // ����
        }

        // ���ӵ� ����
        if (rigid.velocity.x > 5f)                                        // ������ �ӵ� 5 �̻� �Ǹ�
            rigid.velocity = new Vector2(5f, rigid.velocity.y);           // 5�� �ٲ��ֱ�
        else if (rigid.velocity.x < 5f * (-1))                            // ���� �ӵ� 5 �̻� �Ǹ�
            rigid.velocity = new Vector2(5f * (-1), rigid.velocity.y);    // 5�� �ٲ��ֱ�

        
        // �÷��̾�� 0.1 �̻� �Ÿ� ���̳���
        if (distance.x > 0.1f || -0.1f > distance.x)
        {
            // �÷��̾� �������� ������ ����, �̵�
            rigid.AddForce(Vector2.right * head * speed);
            animator.SetBool("IsWalk", true);

            // ���� ��ȯ
            if (distance.x < 0)
            {
                // �÷��̾ ���ʿ� ������
                spriteRenderer.flipX = true;
            }
            else
            {
                // �÷��̾ �����ʿ� ������
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // �÷��̾�� �Ÿ��� 0.1 �����̸� ����
            // rigidbody.velocity.normalized ���� ũ�⸦ 1�� ���� ����(��������) ���� �� 1�� ����
            // 1���� �� �۾�������(�ӵ� �ٵ���), ���� �ӵ��� �״��
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    public void OnEnemyDamaged(Vector3 targetPos)
    {
        // ���� ������ �Ծ��� ��
        // ������ �������ϰ� ������ֱ�
        spriteRenderer.color = new Color(1, 0, 0, 0.8f);

        // ƨ��� ����Ʈ, Ÿ�� �ݴ� �������� ������ ����
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // 3�� �Ŀ� ���� ���·� ���� �޼��� ȣ��
        Invoke("OffEnemyDamaged", 3);


    }
    void OffEnemyDamaged()
    {
        // ������ �۵�X
        if (isDie)
        {
            return;
        }

        // ���� �ʾ����� ���� ���·� ����
        // ���� �������
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void OnDie()
    {
        // ������ isDie�� ����
        isDie = true;

        // �������ϰ� �÷� ����
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // �̹��� �Ųٷ� ������
        spriteRenderer.flipY = true;

        // �ݶ��̴� �����ؼ� �� �հ� �������� �ϱ�
        boxCollider.enabled = false;

        // ���� ƨ��� ��� ����Ʈ �����ϱ�
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 3�� �Ŀ� �ڱ� ��ü ����
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹���� ��

        if (collision.gameObject.tag == "Player")
            return;
        if(collision.gameObject.tag == "Platform")
            return;
        if(collision.gameObject.tag == "PlayerAttack")
        {
            // �Ѿ˰� �ε�����
            // ���� ������ �޼��� ȣ��
            OnEnemyDamaged(collision.transform.position);
        }
    }

}
