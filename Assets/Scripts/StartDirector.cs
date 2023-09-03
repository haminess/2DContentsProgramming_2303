using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    // 시작 씬 진행 스크립트

    // 컴포넌트 불러오기
    StoryDirector storyDirector;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        storyDirector = GetComponent<StoryDirector>();

        // 시작 스토리 출력 메서드 호출
        StartCoroutine(storyDirector.ShowStartStory());
    }

    // Update is called once per frame
    void Update()
    {
        // 스토리가 출력이 끝나면
        if (!storyDirector.isPlaying && Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭하면 스테이지 씬으로 이동
            SceneManager.LoadScene("StageScene");
        }
    }
}
