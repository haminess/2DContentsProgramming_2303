using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾�䳢�����������ϴ¡���ũ��Ʈ
    // �÷��̾� ��ġ: ���� ü��, Ǯ ü��, �ܰŸ� ���ݷ�, ���Ÿ� ���ݷ�
    public static int hp;
    public static int maxHp = 20;
    public static int powers = 10;
    public static int powerl = 5;

    // �ν����� â���� ���� �뵵
    public int hp_show;
    public int maxHp_show;
    public int powers_show;
    public int powerl_show;

    // �÷��̾� ��ġ2: �̵� �ִ�ӵ�, ������, �ܰ��� ��Ÿ��ī��Ʈ�ð�, ������ ��Ÿ��ī��Ʈ�ð�, ��Ÿ�� �ð�
    public float maxSpeed;
    public float jumpPower;
    float times;
    float timel;
    float coolTime = 0.5f;

    // �׾����� üũ, ������ �۵� ����
    public bool isDie = false;

    // �ܰŸ�, ���Ÿ� ���� ����
    public GameObject attack;
    public GameObject bullet;

    // �÷��̾� ������Ʈ �ҷ�����
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;

    // ���ӵ��� �ҷ�����
    public GameDirector gameDirector;
    public SoundDirector soundDirector;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // �ν����� â�� ������ �÷��̾� ��ġ ������Ʈ
        hp_show = hp;
        maxHp_show = maxHp;
        powerl_show = powerl;
        powers_show = powers;

        // �׾����� �۵� ����
        if (hp <= 0 && !isDie)
        {
            OnDie();
            return;
        }

        // �÷��̾� �۵� üũ
        // ����
        Jump();

        // �̵�
        Move();

        // �ܰŸ� ����
        Attack();

        // ���Ÿ� ����
        Shoot();

    }

    // ����
    public void Jump()
    {
        // �����̽��ٸ� ������, animator���� ���� ���� ���°� �ƴ� �� ���� ����
        // -> ���ÿ� ������ ����
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("IsJump")) 
        {
            soundDirector.Play(soundDirector.jump);                         // ���� ȿ���� ����
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);    // ���� �� * ������ ��ŭ �Ͻ����� �������� �����ش�
            animator.SetBool("IsJump", true);                               // �ִϸ����� ���� ���·� ����
        }

        // ���� �������� �� Idle ���·� �ִϸ��̼� ����
        if (rigid.velocity.y < 0) // �÷��̾� �϶� ���� ��
        {
            // UnityEngine�� Debug.DrawRay�� ���� ������ �� Ray �׷���
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            
            // �÷��̾� �������� ���̸� �����Ͽ� ���̿� �浹�� �÷��� �޾ƿ���
            // Raycast�� Ray�� ������Ű�� RaycastHit2D�� ��ȯ, RaycastHit2D�� �浹�� ��ü�� �������� ����
            // �÷��̾� ��ġ rigid.position����, ���� �Ʒ� ��������, ���� 1��ŭ, Platform ���̾ ����
            // ��ü �浹 ������Ʈ ��ȯ
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); 

            // ���̿� ���� �÷����� ���� ��� �ִϸ��̼� ���� ���� ����
            if (rayHit.collider != null && rayHit.distance < 1f) 
            {
                animator.SetBool("IsJump", false);               
            }
        }
    }

    public void Move()
    {
        // Walk �¿� �̵�
        // ���� �¿� �Է¹����� �� -1, �� 1 ��ȯ
        float h = Input.GetAxisRaw("Horizontal");
        // �¿�� 1��ŭ ������ �ֱ�
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // ���ӵ� ����
        // �÷��̾� �ӵ��� �¿�� �ִ�ӵ����� ũ�� �ִ�ӵ��� �ٲ��ֱ�
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        // Ű ���� �� �ӵ� ����
        // ���� �¿� Ű ���� ��
        if (Input.GetButtonUp("Horizontal"))
        {
            // �ִϸ��̼� �ȱ� ����
            animator.SetBool("IsWalk", false);
            // rigidbody.velocity.normalized ���� ũ�⸦ 1�� ���� ����(��������) ���� �� 1�� ����
            // 1���� �� �۾�������(�ӵ� �ٵ���), ���� �ӵ��� �״��
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // ���� ��ȯ
        // ���� �Է� ������
        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("IsWalk", true);                              // �ִϸ��̼� �ȱ� ���� ��ȯ
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;   // ��������Ʈ �¿� ���� �ٲٱ� ���� �� �ް�������
            if (spriteRenderer.flipX)                                      // ���⿡ ���� 1, -1 �ѹ� �� ����
                h = -1;
            else
                h = 1;
        }
    }

    public void Attack()
    {

        // attack �ٰŸ� ����
        // ��Ÿ�� 0���� ������, ��������
        if (times <= 0)
        {
            // ��Ÿ�� ��
            // ���� ��Ȱ��ȭ
            attack.SetActive(false);

            // Z ������ ����
            if (Input.GetKeyDown(KeyCode.Z))
            {
                soundDirector.Play(soundDirector.attack);                      // ���� ȿ����
                attack.SetActive(true);                                        // ���� Ȱ��ȭ
                if (spriteRenderer.flipX)                                      // �÷��̾� ���⿡ ���� ���� ��ġ ����
                    attack.transform.localPosition = new Vector2(-4, -1);       
                else                                                            
                    attack.transform.localPosition = new Vector2(4, -1);        

                Vector2 boxSize = attack.transform.localScale;                                                 // ���� ��ġ
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attack.transform.position, boxSize, 0);     // ���� ������Ʈ �޾ƿͼ� �浹 ������Ʈ �޾ƿ���
                foreach (Collider2D collider in collider2Ds)                                                   // �浹�� ������Ʈ�鸸ŭ ����
                {
                    if (collider.tag == "Enemy")          // �浹�� ������Ʈ�� Enemy�̸�
                    {                                      
                        OnAttack(collider.gameObject);    // ���� ����
                    }
                }
                times = coolTime;                         // ���� ������ ��Ÿ�� ä���ֱ�
            }
        }
        else
        {
            times -= Time.deltaTime;                      // ��Ÿ�� �ȳ������� �ð� �𿩳����� �ϱ�
        }
    }

    public void Shoot()
    {
        // ���Ÿ� ����
        if (timel <= 0)
        {
            // ��Ÿ�� ������
            // shoot ���Ÿ� ����
            if (Input.GetKeyDown(KeyCode.X))                // xŰ ������
            {                                                
                soundDirector.Play(soundDirector.shoot);    // ���� ȿ����
                OnShot(bullet);                             // ����
                timel = coolTime;                           // ��Ÿ�� ����
            }
        }
        else
        {
            timel -= Time.deltaTime;                        // ��Ÿ�� �ȳ������� �ð� ���̱�
        }
    }

    // �浹_�¾��� ��
    public void OnDamaged(GameObject enemy, int power)
    {
        // power��ŭ ü�� ���̱�
        hp -= power;

        // �浹 �ȵǴ� ���̾�� ����
        gameObject.layer = 6;

        // �����ϰ� �����
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // ƨ��� ����
        // �÷��̾ �� �����̸� -1 ��ȯ, �������̸� 1 ��ȯ
        int dirc = transform.position.x - enemy.transform.position.x > 0 ? 1 : -1;
        // �÷��̾� ��ġ �밢�� �������� ������ �ֱ�
        rigid.AddForce(new Vector2(dirc, 1) * 8, ForceMode2D.Impulse);

        // 1�� �� ������� ���� ���̾� ����
        Invoke("OffDamaged", 1);
    }

    public void OffDamaged()
    {
        // �׾����� �۵�X
        if (isDie)
        {
            return;
        }

        // �ٽ� Player ���̾�� ����
        gameObject.layer = 10;
        // ���� �÷��� ����
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(GameObject enemy)
    {
        // �ܰŸ� ���� ȿ��
        // �����ϸ� ��� ���� hp ����ŭ ���
        enemy.GetComponent<Enemy>().hp -= powers;
        // ���� ������ ����Ʈ �۵�
        enemy.GetComponent<Enemy>().OnEnemyDamaged(transform.position);

        // ���� �����ϸ� �÷��̾ ����Ʈ ���� ������ �ֱ�
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }


    void OnShot(GameObject bullet)
    {
        // ���Ÿ� ����
        // �Ѿ� ������ �ʿ� ���������ֱ�
        Vector3 bulletPos = transform.position;                       // �Ѿ� �� ��ġ���� ����
        Quaternion bulletquater = transform.rotation;                 // �Ѿ� ȸ���� ����
        Bullet bulletController = bullet.GetComponent<Bullet>();      // �Ѿ� ������Ʈ �޾ƿͼ�
        bulletController.direction = spriteRenderer.flipX;            // �Ѿ� ���� ����
        bulletController.bulletPower = powerl;                        // �Ѿ� �� ����
        Instantiate(bullet, bulletPos, bulletquater);                 // ������ ������Ʈ �����ϱ�
    }

    public void OnDie()
    {
        // �׾��� ��

        // ���� ����
        isDie = true;

        // �����ϰ� ������ֱ�
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // �������� �����
        spriteRenderer.flipY = true;

        // �ݶ��̴� �����ؼ� �� �հ� �������� �ϱ�
        boxCollider.enabled = false;

        // ��� ����Ʈ�� ���� ������ ����
        rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);

        // ������ �������� ������ �̵��ϴ� �޼��� ȣ��
        StartCoroutine(gameDirector.ExitStage());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹���� ��
        // ���̶� �ε�����
        if(collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                // �� ������ �ε������� ����
                OnAttack(collision.gameObject);
                
                // ƨ��� ����Ʈ, ���������� ���������� 1 ��ȯ, ���� -1 ��ȯ
                int dirc = rigid.velocity.x > 0 ? 1 : -1;
                // ���� �ִ� ���� �밢������ ������ ����
                rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            }
            else // �� ������ �ε����� �ƴϸ� ������
                OnDamaged(collision.gameObject, collision.gameObject.GetComponent<Enemy>().power); // ���� power��ŭ �÷��̾� ü�� ����
        }
    }
}
