using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // 플레이어와 보스 HP UI 관리 스크립트
    // 컴포넌트 불러오기
    public GameDirector gameDirector;

    // 스테이지 UI 오브젝트
    public Slider playerHP;               // 플레이어 HP 슬라이더 UI
    public Slider bossHP;                 // 보스 HP 슬라이더 UI
    public Enemy boss;                    // 보스 오브젝트

    bool stageEnd = false;                // 스테이지 끝났는지 정보


    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        gameDirector = GetComponent<GameDirector>();   
    }

    // Update is called once per frame
    void Update()
    {
        // HP 출력
        ShowHp();
    }

    void ShowHp()
    {
        // HP 출력 메서드

        // UI 슬라이더 값 갱신
        // 슬라이더 value는 0~1이기 때문에
        // hp / maxHp로 계산해서 0~1 사이 값으로 넣어준다
        playerHP.value = (float)Player.hp / Player.maxHp;   // 플레이어 hp값 갱신
        if (!bossHP.enabled)                                // 보스UI 비활성화 되어있으면
            return;                                         // 작동 중지
        bossHP.value = (float)boss.hp / boss.maxHp;         // 활성화 되어있으면 보스 hp값 갱신
        if(boss.hp <= 0 && !stageEnd)                       // 보스 hp 0이하되고 게임이 아직 안끝났으면
        {
            stageEnd = true;                                // 게임 끝내기
            gameDirector.Invoke("ShowEnding", 3);           // 게임디렉터에서 엔딩 보여주는 메서드 3초 뒤에 호출
        }
    }

    public void Show(Image image)
    {
        // 이미지를 FadeIn 시켜주는 메서드
        StartCoroutine(FadeIn(image));
    }


    public void Hide(Image image)
    {
        // 이미지를 FadeOut 시켜주는 메서드
        StartCoroutine(FadeOut(image));
    }

    IEnumerator FadeIn(Image image)
    {
        // FadeIn 메서드
        // Lerp 함수는 찾고 싶은 지점을 비율로 표시하여 값 반환
        float time = 0f;             // 비율로 시간 값 0 설정
        Color color = image.color;   // 오브젝트 컬러 받아오기
        while (color.a < 1f)         // 투명도가 1 이하이면
        {
            time += Time.deltaTime;               // 시간 값 증가하면서
            color.a = Mathf.Lerp(0, 1, time);     // 투명도 1이 될때까지 선형보간
            image.color = color;                  // 투명도 값 넣어주기
            yield return null;
        }
    }
    IEnumerator FadeOut(Image image)
    {
        // FadeOut 메서드
        float time = 0f;              // 비율로 시간 값 0 설정
        Color color = image.color;    // 오브젝트 컬러 받아오기
        while (color.a > 0)           // 투명도가 1 이하이면
        {
            time += Time.deltaTime;               // 시간 값 증가하면서
            color.a = Mathf.Lerp(1, 0, time);     // 투명도 0이 될때까지 선형보간
            image.color = color;                  // 투명도 값 넣어주기
            yield return null;
        }
    }

}
