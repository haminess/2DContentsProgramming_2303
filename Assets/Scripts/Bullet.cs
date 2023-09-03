using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 총알 컨트롤 스크립트

    public bool direction;             // 총알이 나가는 방향
    public int bulletPower;            // 총알 공격력
    public float bulletSpeed = 0.5f;   // 총알 속도

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.4f);     // 0.4초 이후 삭제
    }

    // Update is called once per frame
    void Update()
    {
        if (!direction)                                                // false이면
        {                                                              
            gameObject.transform.Translate(bulletSpeed, 0, 0);         // 오른쪽으로
        }                                                              
        else                                                           // true이면
        {                                                               
            gameObject.transform.Translate(-1 * bulletSpeed, 0, 0);    // 왼쪽으로 좌표 이동 메서드
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            // 몬스터와 충돌하면
            collision.gameObject.GetComponent<Enemy>().hp -= bulletPower;                    // 몬스터 체력 깎기
            collision.gameObject.GetComponent<Enemy>().OnEnemyDamaged(transform.position);   // 데미지 이펙트 발생
            Destroy(gameObject);                                                             // 자기 총알은 삭제
        }
        if(collision.tag == "Player")
        {
            // 플레이어와 충돌하면
            collision.gameObject.GetComponent<Player>().OnDamaged(gameObject, bulletPower);  // 플레이어 체력 깎기
            Destroy(gameObject);                                                             // 총알 삭제
        }
    }

}
