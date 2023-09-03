using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어（토끼）를　관리하는　스크립트
    // 플레이어 수치: 현재 체력, 풀 체력, 단거리 공격력, 원거리 공격력
    public static int hp;
    public static int maxHp = 20;
    public static int powers = 10;
    public static int powerl = 5;

    // 인스펙터 창에서 보는 용도
    public int hp_show;
    public int maxHp_show;
    public int powers_show;
    public int powerl_show;

    // 플레이어 수치2: 이동 최대속도, 점프력, 단공격 쿨타임카운트시간, 원공격 쿨타임카운트시간, 쿨타임 시간
    public float maxSpeed;
    public float jumpPower;
    float times;
    float timel;
    float coolTime = 0.5f;

    // 죽었는지 체크, 죽으면 작동 멈춤
    public bool isDie = false;

    // 단거리, 원거리 공격 무기
    public GameObject attack;
    public GameObject bullet;

    // 플레이어 컴포넌트 불러오기
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;

    // 게임디렉터 불러오기
    public GameDirector gameDirector;
    public SoundDirector soundDirector;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // 인스펙터 창에 보여줄 플레이어 수치 업데이트
        hp_show = hp;
        maxHp_show = maxHp;
        powerl_show = powerl;
        powers_show = powers;

        // 죽었으면 작동 중지
        if (hp <= 0 && !isDie)
        {
            OnDie();
            return;
        }

        // 플레이어 작동 체크
        // 점프
        Jump();

        // 이동
        Move();

        // 단거리 공격
        Attack();

        // 원거리 공격
        Shoot();

    }

    // 점프
    public void Jump()
    {
        // 스페이스바를 누르고, animator에서 현재 점프 상태가 아닐 때 점프 실행
        // -> 동시에 점프만 가능
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("IsJump")) 
        {
            soundDirector.Play(soundDirector.jump);                         // 점프 효과음 실행
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);    // 벡터 위 * 점프력 만큼 일시적인 물리힘을 가해준다
            animator.SetBool("IsJump", true);                               // 애니메이터 점프 상태로 변경
        }

        // 점프 착지했을 때 Idle 상태로 애니메이션 변경
        if (rigid.velocity.y < 0) // 플레이어 하락 중일 때
        {
            // UnityEngine의 Debug.DrawRay를 통해 에디터 상에 Ray 그려줌
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            
            // 플레이어 기준으로 레이를 생성하여 레이와 충돌한 플랫폼 받아오기
            // Raycast는 Ray를 생성시키고 RaycastHit2D를 반환, RaycastHit2D는 충돌된 객체의 정보들을 담음
            // 플레이어 위치 rigid.position에서, 벡터 아래 방향으로, 길이 1만큼, Platform 레이어를 가진
            // 객체 충돌 오브젝트 반환
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform")); 

            // 레이에 닿은 플랫폼이 있을 경우 애니메이션 점프 상태 해제
            if (rayHit.collider != null && rayHit.distance < 1f) 
            {
                animator.SetBool("IsJump", false);               
            }
        }
    }

    public void Move()
    {
        // Walk 좌우 이동
        // 수평 좌우 입력받으면 좌 -1, 우 1 반환
        float h = Input.GetAxisRaw("Horizontal");
        // 좌우로 1만큼 물리힘 주기
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 가속도 제한
        // 플레이어 속도가 좌우로 최대속도보다 크면 최대속도로 바꿔주기
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        // 키 뗐을 때 속도 제한
        // 수평 좌우 키 땠을 때
        if (Input.GetButtonUp("Horizontal"))
        {
            // 애니메이션 걷기 해제
            animator.SetBool("IsWalk", false);
            // rigidbody.velocity.normalized 벡터 크기를 1로 만든 상태(단위벡터) 벡터 값 1로 만듬
            // 1에서 더 작아지도록(속도 줄도록), 상하 속도는 그대로
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // 방향 전환
        // 수평 입력 들어오면
        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("IsWalk", true);                              // 애니메이션 걷기 상태 전환
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;   // 스프라이트 좌우 방향 바꾸기 수평 값 받고있으면
            if (spriteRenderer.flipX)                                      // 방향에 따라 1, -1 한번 더 세팅
                h = -1;
            else
                h = 1;
        }
    }

    public void Attack()
    {

        // attack 근거리 공격
        // 쿨타임 0보다 작으면, 끝났으면
        if (times <= 0)
        {
            // 쿨타임 끝
            // 무기 비활성화
            attack.SetActive(false);

            // Z 누르면 공격
            if (Input.GetKeyDown(KeyCode.Z))
            {
                soundDirector.Play(soundDirector.attack);                      // 공격 효과음
                attack.SetActive(true);                                        // 공격 활성화
                if (spriteRenderer.flipX)                                      // 플레이어 방향에 따라 공격 위치 조정
                    attack.transform.localPosition = new Vector2(-4, -1);       
                else                                                            
                    attack.transform.localPosition = new Vector2(4, -1);        

                Vector2 boxSize = attack.transform.localScale;                                                 // 공격 위치
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attack.transform.position, boxSize, 0);     // 공격 오브젝트 받아와서 충돌 오브젝트 받아오기
                foreach (Collider2D collider in collider2Ds)                                                   // 충돌된 오브젝트들만큼 실행
                {
                    if (collider.tag == "Enemy")          // 충돌된 오브젝트가 Enemy이면
                    {                                      
                        OnAttack(collider.gameObject);    // 공격 실행
                    }
                }
                times = coolTime;                         // 공격 했으면 쿨타임 채워주기
            }
        }
        else
        {
            times -= Time.deltaTime;                      // 쿨타임 안끝났으면 시간 깎여나가게 하기
        }
    }

    public void Shoot()
    {
        // 원거리 공격
        if (timel <= 0)
        {
            // 쿨타임 끝나고
            // shoot 원거리 공격
            if (Input.GetKeyDown(KeyCode.X))                // x키 누르면
            {                                                
                soundDirector.Play(soundDirector.shoot);    // 공격 효과음
                OnShot(bullet);                             // 공격
                timel = coolTime;                           // 쿨타임 설정
            }
        }
        else
        {
            timel -= Time.deltaTime;                        // 쿨타임 안끝났으면 시간 줄이기
        }
    }

    // 충돌_맞았을 때
    public void OnDamaged(GameObject enemy, int power)
    {
        // power만큼 체력 줄이기
        hp -= power;

        // 충돌 안되는 레이어로 변경
        gameObject.layer = 6;

        // 투명하게 만들기
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 튕기는 반응
        // 플레이어가 적 왼쪽이면 -1 반환, 오른쪽이면 1 반환
        int dirc = transform.position.x - enemy.transform.position.x > 0 ? 1 : -1;
        // 플레이어 위치 대각선 방향으로 물리힘 주기
        rigid.AddForce(new Vector2(dirc, 1) * 8, ForceMode2D.Impulse);

        // 1초 후 무적모드 해제 레이어 변경
        Invoke("OffDamaged", 1);
    }

    public void OffDamaged()
    {
        // 죽엇으면 작동X
        if (isDie)
        {
            return;
        }

        // 다시 Player 레이어로 변경
        gameObject.layer = 10;
        // 정상 컬러로 변경
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(GameObject enemy)
    {
        // 단거리 공격 효과
        // 공격하면 상대 몬스터 hp 힘만큼 깎기
        enemy.GetComponent<Enemy>().hp -= powers;
        // 몬스터 데미지 이펙트 작동
        enemy.GetComponent<Enemy>().OnEnemyDamaged(transform.position);

        // 공격 성공하면 플레이어도 이펙트 위로 물리힘 주기
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }


    void OnShot(GameObject bullet)
    {
        // 원거리 공격
        // 총알 프리팹 맵에 생성시켜주기
        Vector3 bulletPos = transform.position;                       // 총알 내 위치에서 시작
        Quaternion bulletquater = transform.rotation;                 // 총알 회전값 설정
        Bullet bulletController = bullet.GetComponent<Bullet>();      // 총알 컴포넌트 받아와서
        bulletController.direction = spriteRenderer.flipX;            // 총알 방향 설정
        bulletController.bulletPower = powerl;                        // 총알 힘 설정
        Instantiate(bullet, bulletPos, bulletquater);                 // 프리팹 오브젝트 생성하기
    }

    public void OnDie()
    {
        // 죽었을 때

        // 상태 갱신
        isDie = true;

        // 투명하게 만들어주기
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 뒤집히게 만들기
        spriteRenderer.flipY = true;

        // 콜라이더 해제해서 땅 뚫고 떨어지게 하기
        boxCollider.enabled = false;

        // 사망 이펙트로 위로 물리힘 적용
        rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);

        // 죽으면 스테이지 씬으로 이동하는 메서드 호출
        StartCoroutine(gameDirector.ExitStage());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌했을 때
        // 적이랑 부딪히면
        if(collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                // 적 위에서 부딪혔으면 공격
                OnAttack(collision.gameObject);
                
                // 튕기는 이펙트, 오른쪽으로 가고있으면 1 반환, 왼쪽 -1 반환
                int dirc = rigid.velocity.x > 0 ? 1 : -1;
                // 가고 있던 방향 대각선으로 물리힘 적용
                rigid.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);
            }
            else // 적 위에서 부딪힌게 아니면 데미지
                OnDamaged(collision.gameObject, collision.gameObject.GetComponent<Enemy>().power); // 적의 power만큼 플레이어 체력 깎임
        }
    }
}
