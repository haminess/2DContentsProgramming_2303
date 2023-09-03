using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDirector : MonoBehaviour
{
    // 스토리를 관리하는 스크립트


    public Image back;                                // 스토리 배경 이미지
    public Image black;                               // 검은 화면 이미지
    public Image[] title = new Image[3];              // 시작 화면 이미지
    public Image[] startStory = new Image[10];        // 시작 스토리 이미지
    public Image[] stageStory1 = new Image[6];        // 1스테이지 스토리 이미지
    public Image[] stageStory2 = new Image[6];        // 2스테이지 스토리 이미지
    public Image[] stageStory3 = new Image[15];       // 3스테이지 스토리 이미지

    public bool isPlaying = false;                    // 스토리 실행 중인지

    public float hideTime = 2f;                       // 출력 시간 조정: 빈 시간
    public float showTime = 3.5f;                     // 출력 시간 조정: 보여주는 시간

    SoundDirector soundDirector;                      // 사운드 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 사운드 컴포넌트 불러오기
        soundDirector = GetComponent<SoundDirector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(Image image)
    {
        // fadeIn 적용 메서드
        StartCoroutine(FadeIn(image));
    }
    public void Hide(Image image)
    {
        // fadeOut 적용 메서드
        StartCoroutine(FadeOut(image));
    }
    public void ShowBlack()
    {
        // 검은화면 fadein 메서드
        StartCoroutine(FadeIn(black));
    }
    public void HideBlack()
    {
        // 검은 화면 fadeout 메서드
        StartCoroutine(FadeOut(black));
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

    public IEnumerator ShowStartStory()
    {
        // 시작 스토리 출력
        isPlaying = true;     // 스토리 실행 시작
        HideBlack();          // 검은 화면 fadeout

        // 배경 출력
        soundDirector.Play(soundDirector.sounds[0]);     // 효과음
        Show(back);                                      // 배경 fadein
        yield return new WaitForSeconds(hideTime);       // 대기

        // 장면1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[0]);
        Show(startStory[1]);
        yield return new WaitForSeconds(hideTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[2]);
        Show(startStory[3]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(startStory[0]);
        Hide(startStory[1]);
        Hide(startStory[2]);
        Hide(startStory[3]);
        yield return new WaitForSeconds(hideTime);

        // 장면2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[4]);
        Show(startStory[5]);
        yield return new WaitForSeconds(hideTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[6]);
        Show(startStory[7]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(startStory[4]);
        Hide(startStory[5]);
        Hide(startStory[6]);
        Hide(startStory[7]);
        yield return new WaitForSeconds(hideTime);

        // 장면3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[8]);
        yield return new WaitForSeconds(hideTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(startStory[9]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(startStory[8]);
        Hide(startStory[9]);
        Hide(back);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);

        // 시작화면 출력
        HideBlack();
        Show(title[0]);
        Show(title[1]);
        Show(title[2]);

        // 실행 끝
        isPlaying = false;

        // 하단 텍스트 반짝반짝 이펙트
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            title[2].color = Color.clear;
            yield return new WaitForSeconds(0.5f);
            title[2].color = Color.white;
        }
    }

    public IEnumerator ShowStage1Story()
    {
        // 스테이지1 스토리 출력
        // 스토리 실행
        isPlaying = true;
        Show(back);
        yield return new WaitForSeconds(1f);
        HideBlack();
        soundDirector.Play(soundDirector.sounds[0]);
        yield return new WaitForSeconds(1f);

        // 장면1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[1]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[0]);
        Hide(stageStory1[1]);

        // 장면2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[2]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[2]);

        // 장면3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[3]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[3]);

        // 장면4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[4]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[4]);

        // 끝 검은 화면 출력
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 게임 시작 메서드 호출
        GetComponent<GameDirector>().StartStage();

        yield return null;
    }
    public IEnumerator ShowStage1Ending()
    {
        // 스테이지1 엔딩 출력
        // 스토리 실행
        isPlaying = true;

        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // 엔딩 출력
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[stageStory1.Length - 1]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[stageStory1.Length - 1]);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);
        back.color = new Color(1, 1, 1, 0);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 스테이지 화면으로 넘어가기
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;
    }
    public IEnumerator ShowStage2Story()
    {
        // 스토리 실행
        isPlaying = true;
        Show(back);
        yield return new WaitForSeconds(1f);
        HideBlack();
        soundDirector.Play(soundDirector.sounds[0]);
        yield return new WaitForSeconds(1f);

        // 장면1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[0]);
        Hide(stageStory2[1]);

        // 장면2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[2]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[3]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[3]);
        Hide(stageStory2[2]);

        // 장면3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[4]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[4]);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 게임 시작
        GetComponent<GameDirector>().StartStage();
        yield return null;
    }
    public IEnumerator ShowStage2Ending()
    {
        // 검은 화면
        isPlaying = true;
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // 엔딩 출력
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[stageStory2.Length - 1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[stageStory2.Length - 1]);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);
        back.color = new Color(1, 1, 1, 0);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 스테이지 씬으로 돌아가기
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;

    }
    public IEnumerator ShowStage3Story()
    {
        // 스토리 출력
        isPlaying = true;

        // 검은 화면
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // 장면1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[1]);
        Hide(stageStory3[0]);

        // 장면2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[2]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[2]);

        // 장면3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[3]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[3]);

        // 장면4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[4]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[4]);

        // 장면5
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[5]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[5]);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 게임시작
        GetComponent<GameDirector>().StartStage();
        yield return null;
    }
    public IEnumerator ShowGameEnding()
    {
        // 스토리 시작
        isPlaying = true;

        // 검은 화면
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // 엔딩 출력, 장면1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[6]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[7]);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[8]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[7]);
        Hide(stageStory3[6]);
        Hide(stageStory3[8]);

        // 장면2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[9]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[10]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[9]);
        Hide(stageStory3[10]);

        // 장면3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[11]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[12]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[13]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[11]);
        Hide(stageStory3[12]);
        Hide(stageStory3[13]);

        // 장면4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[14]);
        yield return new WaitForSeconds(10);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[14]);

        // 검은 화면
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // 스토리 끝
        isPlaying = false;

        // 스테이지 씬으로 돌아가기
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;
    }
}
