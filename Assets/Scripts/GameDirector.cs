using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // 스테이지 게임 진행 스크립트
    
    // 컴포넌트 불러오기
    StoryDirector storyDirector;

    // 게임 정보
    public static bool[] stageStory = { false, false, false, false };      // 스테이지 별로 스토리를 봤었는지 체크, 본 스토리는 다시 보지 않는다
    public bool[] stageStory_show;                                         // 인스펙터 창에서 보는 용도
    public int stageNum;                                                   // 스테이지 번호
    public GameObject player;                                              // 플레이어 오브젝트
    public GameObject boss;                                                // 보스 오브젝트
    public GameObject camera0;                                             // 시작 카메라 오브젝트

    

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 초기화
        storyDirector = GetComponent<StoryDirector>();

        Player.hp = Player.maxHp;      // 플레이어 체력 채우기

        // 게임 시작
        if (!stageStory[stageNum])
        {
            // 스토리 안봤으면 스토리 출력
            ShowStartStory();
        }
        else
        {
            // 스토리 봤으면 바로 게임 시작
            storyDirector.HideBlack();    // 검정 화면에서 스테이지로 FadeIn 효과
            StartStage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 인스펙터 창에서 볼 수 있도록 수치 갱신
        stageStory_show = stageStory;
    }

    // 스토리 출력 메서드
    void ShowStartStory()
    {
        // 스테이지 별 스토리 출력 메서드
        switch (stageNum)
        {
            case 1:
                StartCoroutine(storyDirector.ShowStage1Story());     // 1스테이지면 스토리1 출력
                stageStory[stageNum] = true;
                break;
            case 2:
                StartCoroutine(storyDirector.ShowStage2Story());     // 2스테이지면 스토리2 출력
                stageStory[stageNum] = true;
                break;
            case 3:
                StartCoroutine(storyDirector.ShowStage3Story());     // 3스테이지면 스토리3 출력
                stageStory[stageNum] = true;                         // 이후 스토리 읽은 상태 체크
                break;
        }
    }
    // 엔딩 출력 메서드
    public void ShowEnding()
    {
        // 스테이지 별 엔딩 출력 메서드
        switch (stageNum)
        {
            case 1:
                StartCoroutine(storyDirector.ShowStage1Ending());    // 1스테이지 엔딩 출력
                break;
            case 2:
                StartCoroutine(storyDirector.ShowStage2Ending());    // 2스테이지 엔딩 출력
                break;
            case 3:
                StartCoroutine(storyDirector.ShowGameEnding());    // 3스테이지 엔딩 출력
                break;
        }
    }
    // 게임 시작 메서드
    public void StartStage()
    {
        // 본 게임 시작 메서드
        player.SetActive(true);        // 플레이어 활성화
        if (!boss.activeSelf)          // 보스 비활성화 되어있으면
        {
            boss.SetActive(true);      // 보스 활성화
        }                              // 1스테이지에서만
        if(stageNum == 1)
        {
            camera0.transform.SetParent(player.transform);    // 카메라가 플레이어를 따라가도록 설정
        }
    }
    // 게임 탈출 메서드
    public IEnumerator ExitStage()
    {
        // 스테이지로 돌아가는 메서드, 코루틴 함수라서 IEnumerator 반환
        yield return new WaitForSeconds(3);        // 3초 후
        storyDirector.ShowBlack();                 // 검은 화면 FadeOut 효과에
        yield return new WaitForSeconds(1);        // 1초 후
        SceneManager.LoadScene("StageScene");      // 스테이지 씬으로 전환
    }
    // 추락 시 작동 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // 플레이어가 바닥과 충돌했을 시
            // 체력 감소
            Player.hp -= 10;
            // 위에서 다시 소환
            player.transform.position = new Vector2(player.transform.position.x, 20);

        }
    }
}
