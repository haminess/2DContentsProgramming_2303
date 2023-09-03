using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 몬스터 관리 스크립트
    // 컴포넌트 불러오기
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public BoxCollider2D boxCollider;

    // 플레이어 오브젝트 불러오기
    public GameObject player;

    // 몬스터 정보
    public int hp;                 // 현재 체력
    public int maxHp;              // 최대 체력
    public int power;              // 닿았을 때 힘
    public int attackPower;        // 공격력
    public int speed;              // 이동 속도
    public bool isDie = false;     // 죽었는지 상태

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 초기화
        player = GameObject.Find("Bunny");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        // 시작할 때 체력 채워주기
        hp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        // 죽지 않았는데 체력이 0 이하가 되면
        if(hp <= 0 && !isDie)
        {
            // 죽음
            OnDie();
            return;
        }

        // 죽어도 작동 멈춤
        if (isDie)
        {
            return;
        }

        // 이동
        Move();
        
    }

    void Move()
    {
        // 몬스터 이동 메서드
        // 플레이어 비활성화 되어있으면 멈춤
        if (!player)    
        {
            return;
        }

        // 플레이어와 몬스터 사이 거리 계산
        Vector2 distance = player.transform.position - transform.position;
        // 플레이어가 왼쪽에 있으면 -1, 오른쪽 1
        int head = distance.x > 0 ? 1 : -1;


        // 바닥과 닿으면 이동, 바닥이 없으면 멈춤
        Vector2 frontVec = new Vector2(rigid.position.x + head * 1f, rigid.position.y);
        // 몬스터 기준으로 레이를 생성하여 레이와 충돌한 플랫폼이 있는지 체크하고 받아오기
        // Raycast는 Ray를 생성시키고 RaycastHit2D를 반환, RaycastHit2D는 충돌된 객체의 정보들을 담음
        // 몬스터 위치보다 1 앞에서, 벡터 아래 방향으로, 길이 10만큼, Platform 레이어를 가진
        // 객체 충돌 오브젝트 반환
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 10, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)     // 앞에 충돌된 플랫폼이 없으면
        {                                 
            return;                      // 멈춤
        }

        // 가속도 제한
        if (rigid.velocity.x > 5f)                                        // 오른쪽 속도 5 이상 되면
            rigid.velocity = new Vector2(5f, rigid.velocity.y);           // 5로 바꿔주기
        else if (rigid.velocity.x < 5f * (-1))                            // 왼쪽 속도 5 이상 되면
            rigid.velocity = new Vector2(5f * (-1), rigid.velocity.y);    // 5로 바꿔주기

        
        // 플레이어와 0.1 이상 거리 차이나면
        if (distance.x > 0.1f || -0.1f > distance.x)
        {
            // 플레이어 방향으로 물리힘 적용, 이동
            rigid.AddForce(Vector2.right * head * speed);
            animator.SetBool("IsWalk", true);

            // 방향 전환
            if (distance.x < 0)
            {
                // 플레이어가 왼쪽에 있으면
                spriteRenderer.flipX = true;
            }
            else
            {
                // 플레이어가 오른쪽에 있으면
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // 플레이어와 거리가 0.1 이하이면 멈춤
            // rigidbody.velocity.normalized 벡터 크기를 1로 만든 상태(단위벡터) 벡터 값 1로 만듬
            // 1에서 더 작아지도록(속도 줄도록), 상하 속도는 그대로
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    public void OnEnemyDamaged(Vector3 targetPos)
    {
        // 몬스터 데미지 입었을 때
        // 빨간색 불투명하게 만들어주기
        spriteRenderer.color = new Color(1, 0, 0, 0.8f);

        // 튕기는 이펙트, 타겟 반대 방향으로 물리힘 적용
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // 3초 후에 원래 상태로 변경 메서드 호출
        Invoke("OffEnemyDamaged", 3);


    }
    void OffEnemyDamaged()
    {
        // 죽으면 작동X
        if (isDie)
        {
            return;
        }

        // 죽지 않았으면 원래 상태로 변경
        // 색깔 원래대로
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    public void OnDie()
    {
        // 죽으면 isDie값 갱신
        isDie = true;

        // 불투명하게 컬러 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 이미지 거꾸로 뒤집기
        spriteRenderer.flipY = true;

        // 콜라이더 해제해서 땅 뚫고 떨어지게 하기
        boxCollider.enabled = false;

        // 위로 튕기는 사망 이펙트 적용하기
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // 3초 후에 자기 객체 삭제
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌했을 때

        if (collision.gameObject.tag == "Player")
            return;
        if(collision.gameObject.tag == "Platform")
            return;
        if(collision.gameObject.tag == "PlayerAttack")
        {
            // 총알과 부딪히면
            // 몬스터 데미지 메서드 호출
            OnEnemyDamaged(collision.transform.position);
        }
    }

}
