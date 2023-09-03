using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    // 원숭이 전용 공격 스크립트
    // 원숭이 컴포넌트
    public Animator animator;    
    
    // 원숭이 수치
    public Enemy monkey;      // 자기 컴포넌트
    public GameObject banana; // 원숭이 무기
    public int attackTime;    // 바나나 총알 던질 시간
    public int attackPower;   // 바나나 총알 힘

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 초기화
        animator = GetComponent<Animator>();
        monkey = GetComponent<Enemy>();

        // 바나나 언제 던질지 생각하는 함수
        Think();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // 다음 행동 랜덤 지정
    // 재귀함수
    void Think()
    {
        // 다음 공격 시간 랜덤 할당
        attackTime = Random.Range(1, 5);

        // 다음 생각할 시간 랜덤 할당
        float nextThinkTime = Random.Range(2f, 5f);

        // 원숭이 공격 메서드 실행, 생각 함수 실행
        // Invoke 함수는 string 함수를 int 시간 후에 호출해준다
        Invoke("MonkeyAttack", attackTime);
        Invoke("Think", nextThinkTime);

    }

    void MonkeyAttack()
    {
        // 원숭이 바나나 공격
        monkey.animator.SetTrigger("Attack");                        // 공격 애니메이션 On
        Vector2 bulletPos = transform.position;                      // 총알 위치 지정
        Quaternion bulletquater = transform.rotation;                // 총알 회전 지정
        Bullet bulletController = banana.GetComponent<Bullet>();     // 총알 컴포넌트 불러오기
        bulletController.direction = monkey.spriteRenderer.flipX;    // 총알 방향 지정
        bulletController.bulletPower = attackPower;                  // 총알 힘 지정
        Instantiate(banana, bulletPos, bulletquater);                // 총알 생성, 쏘기
    }

}
