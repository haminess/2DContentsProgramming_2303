using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    // 스테이지 씬 진행 스크립트
    // 스테이지 씬의 집 오브젝트에 적용된 스크립트
    // 스테이지 씬의 지도 오브젝트에 적용된 스크립트

    public GameObject map;                 // 지도 오브젝트
    public GameObject mapInfo;             // 지도 켜라고 알려주는 오브젝트
    public GameObject home;                // 충돌 처리할 집 오브젝트

    // 컴포넌트 불러오기
    public StageDirector stageDirector;    
    public StoryDirector storyDirector;    
    public BoxCollider2D homeCollider;     

    // Start is called before the first frame update
    void Start()
    {
        // 맵 스테이지디렉터 오브젝트에 있는 컴포넌트 불러오기
        stageDirector = GameObject.Find("StageDirector").GetComponent<StageDirector>();
        
        // 스테이지 오브젝트만 스테이지 컴포넌트 불러오기
        if(gameObject.name == "StageDirector")
        {
            storyDirector = GetComponent<StoryDirector>();
            // 검은 화면 페이드 아웃 효과
            storyDirector.Hide(storyDirector.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 아래 화살표 누르면 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 지도 오브젝트 비활성화
            map.SetActive(false);

            // 집 콜라이더 활성화해서 다시 지도 활성화 시킬 수 있게 하기
            stageDirector.homeCollider.enabled = true;
        }
    }

    private void OnMouseEnter()
    {
        // 원숭이 숲, 멧돼지 숲, 사자 숲에 마우스를 가져다 대면 크기 키우기 효과
        if (gameObject.name == "map_monkey" || gameObject.name == "map_boar" || gameObject.name == "map_lion")
            transform.localScale = Vector2.one * 1.05f;
    }
    private void OnMouseExit()
    {
        // 원숭이 숲, 멧돼지 숲, 사자 숲에서 마우스를 때면 원래 크기 전환
        if (gameObject.name == "map_monkey" || gameObject.name == "map_boar" || gameObject.name == "map_lion")
            transform.localScale = Vector2.one * 1.00f;
    }

    private IEnumerator OnMouseDown()
    {
        // 지도의 각 숲을 클릭하면
        // show 메서드 호출로 검은 화면 FadeIn 효과
        // 1.5초 후 해당 스테이지 이동
        if(gameObject.name == "map_monkey")
        {
            stageDirector.storyDirector.Show(stageDirector.storyDirector.black);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Stage1");
        }
        else if(gameObject.name == "map_boar")
        {
            stageDirector.storyDirector.Show(stageDirector.storyDirector.black);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Stage2");
        }
        else if(gameObject.name == "map_lion")
        {
            stageDirector.storyDirector.Show(stageDirector.storyDirector.black);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Stage3");
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 집과 접촉된 상태로
        if (Input.GetKeyDown(KeyCode.UpArrow))            // 위 방향키를 누르면
        {
            map.SetActive(true);                          // 지도 활성화
            stageDirector.homeCollider.enabled = false;   // 집 오브젝트 콜라이더 해제(지도 크기 변화 작동에 영향을 줘서 해제함)
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 집과 접촉하면 지도 활성화 정보 스프라이트 활성화
        stageDirector.mapInfo.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 집과 떨어지면 지도 활성화 정보 스프라이트 비활성화
        stageDirector.mapInfo.SetActive(false);

    }
}
