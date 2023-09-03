using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDirector : MonoBehaviour
{
    // ���丮�� �����ϴ� ��ũ��Ʈ


    public Image back;                                // ���丮 ��� �̹���
    public Image black;                               // ���� ȭ�� �̹���
    public Image[] title = new Image[3];              // ���� ȭ�� �̹���
    public Image[] startStory = new Image[10];        // ���� ���丮 �̹���
    public Image[] stageStory1 = new Image[6];        // 1�������� ���丮 �̹���
    public Image[] stageStory2 = new Image[6];        // 2�������� ���丮 �̹���
    public Image[] stageStory3 = new Image[15];       // 3�������� ���丮 �̹���

    public bool isPlaying = false;                    // ���丮 ���� ������

    public float hideTime = 2f;                       // ��� �ð� ����: �� �ð�
    public float showTime = 3.5f;                     // ��� �ð� ����: �����ִ� �ð�

    SoundDirector soundDirector;                      // ���� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // ���� ������Ʈ �ҷ�����
        soundDirector = GetComponent<SoundDirector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(Image image)
    {
        // fadeIn ���� �޼���
        StartCoroutine(FadeIn(image));
    }
    public void Hide(Image image)
    {
        // fadeOut ���� �޼���
        StartCoroutine(FadeOut(image));
    }
    public void ShowBlack()
    {
        // ����ȭ�� fadein �޼���
        StartCoroutine(FadeIn(black));
    }
    public void HideBlack()
    {
        // ���� ȭ�� fadeout �޼���
        StartCoroutine(FadeOut(black));
    }

    IEnumerator FadeIn(Image image)
    {
        // FadeIn �޼���
        // Lerp �Լ��� ã�� ���� ������ ������ ǥ���Ͽ� �� ��ȯ
        float time = 0f;             // ������ �ð� �� 0 ����
        Color color = image.color;   // ������Ʈ �÷� �޾ƿ���
        while (color.a < 1f)         // ������ 1 �����̸�
        {
            time += Time.deltaTime;               // �ð� �� �����ϸ鼭
            color.a = Mathf.Lerp(0, 1, time);     // ���� 1�� �ɶ����� ��������
            image.color = color;                  // ���� �� �־��ֱ�
            yield return null;
        }
    }
    IEnumerator FadeOut(Image image)
    {
        // FadeOut �޼���
        float time = 0f;              // ������ �ð� �� 0 ����
        Color color = image.color;    // ������Ʈ �÷� �޾ƿ���
        while (color.a > 0)           // ������ 1 �����̸�
        {
            time += Time.deltaTime;               // �ð� �� �����ϸ鼭
            color.a = Mathf.Lerp(1, 0, time);     // ���� 0�� �ɶ����� ��������
            image.color = color;                  // ���� �� �־��ֱ�
            yield return null;
        }
    }

    public IEnumerator ShowStartStory()
    {
        // ���� ���丮 ���
        isPlaying = true;     // ���丮 ���� ����
        HideBlack();          // ���� ȭ�� fadeout

        // ��� ���
        soundDirector.Play(soundDirector.sounds[0]);     // ȿ����
        Show(back);                                      // ��� fadein
        yield return new WaitForSeconds(hideTime);       // ���

        // ���1
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

        // ���2
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

        // ���3
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

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);

        // ����ȭ�� ���
        HideBlack();
        Show(title[0]);
        Show(title[1]);
        Show(title[2]);

        // ���� ��
        isPlaying = false;

        // �ϴ� �ؽ�Ʈ ��¦��¦ ����Ʈ
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
        // ��������1 ���丮 ���
        // ���丮 ����
        isPlaying = true;
        Show(back);
        yield return new WaitForSeconds(1f);
        HideBlack();
        soundDirector.Play(soundDirector.sounds[0]);
        yield return new WaitForSeconds(1f);

        // ���1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[1]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[0]);
        Hide(stageStory1[1]);

        // ���2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[2]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[2]);

        // ���3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[3]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[3]);

        // ���4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[4]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[4]);

        // �� ���� ȭ�� ���
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // ���� ���� �޼��� ȣ��
        GetComponent<GameDirector>().StartStage();

        yield return null;
    }
    public IEnumerator ShowStage1Ending()
    {
        // ��������1 ���� ���
        // ���丮 ����
        isPlaying = true;

        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // ���� ���
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory1[stageStory1.Length - 1]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory1[stageStory1.Length - 1]);

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);
        back.color = new Color(1, 1, 1, 0);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // �������� ȭ������ �Ѿ��
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;
    }
    public IEnumerator ShowStage2Story()
    {
        // ���丮 ����
        isPlaying = true;
        Show(back);
        yield return new WaitForSeconds(1f);
        HideBlack();
        soundDirector.Play(soundDirector.sounds[0]);
        yield return new WaitForSeconds(1f);

        // ���1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[0]);
        Hide(stageStory2[1]);

        // ���2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[2]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[3]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[3]);
        Hide(stageStory2[2]);

        // ���3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[4]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[4]);

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // ���� ����
        GetComponent<GameDirector>().StartStage();
        yield return null;
    }
    public IEnumerator ShowStage2Ending()
    {
        // ���� ȭ��
        isPlaying = true;
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // ���� ���
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory2[stageStory2.Length - 1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory2[stageStory2.Length - 1]);

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);
        back.color = new Color(1, 1, 1, 0);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // �������� ������ ���ư���
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;

    }
    public IEnumerator ShowStage3Story()
    {
        // ���丮 ���
        isPlaying = true;

        // ���� ȭ��
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // ���1
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[0]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[1]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[1]);
        Hide(stageStory3[0]);

        // ���2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[2]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[2]);

        // ���3
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[3]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[3]);

        // ���4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[4]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[4]);

        // ���5
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[5]);
        yield return new WaitForSeconds(showTime);
        Hide(stageStory3[5]);

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // ���ӽ���
        GetComponent<GameDirector>().StartStage();
        yield return null;
    }
    public IEnumerator ShowGameEnding()
    {
        // ���丮 ����
        isPlaying = true;

        // ���� ȭ��
        ShowBlack();
        yield return new WaitForSeconds(1f);
        back.color = Color.white;
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(black);
        yield return new WaitForSeconds(1f);

        // ���� ���, ���1
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

        // ���2
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[9]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[10]);
        yield return new WaitForSeconds(showTime);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[9]);
        Hide(stageStory3[10]);

        // ���3
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

        // ���4
        soundDirector.Play(soundDirector.sounds[1]);
        Show(stageStory3[14]);
        yield return new WaitForSeconds(10);
        soundDirector.Play(soundDirector.sounds[0]);
        Hide(stageStory3[14]);

        // ���� ȭ��
        Show(black);
        yield return new WaitForSeconds(hideTime);
        Hide(back);
        HideBlack();

        // ���丮 ��
        isPlaying = false;

        // �������� ������ ���ư���
        StartCoroutine(GetComponent<GameDirector>().ExitStage());
        yield return null;
    }
}
