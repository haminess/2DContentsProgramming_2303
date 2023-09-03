using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // �÷��̾�� ���� HP UI ���� ��ũ��Ʈ
    // ������Ʈ �ҷ�����
    public GameDirector gameDirector;

    // �������� UI ������Ʈ
    public Slider playerHP;               // �÷��̾� HP �����̴� UI
    public Slider bossHP;                 // ���� HP �����̴� UI
    public Enemy boss;                    // ���� ������Ʈ

    bool stageEnd = false;                // �������� �������� ����


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        gameDirector = GetComponent<GameDirector>();   
    }

    // Update is called once per frame
    void Update()
    {
        // HP ���
        ShowHp();
    }

    void ShowHp()
    {
        // HP ��� �޼���

        // UI �����̴� �� ����
        // �����̴� value�� 0~1�̱� ������
        // hp / maxHp�� ����ؼ� 0~1 ���� ������ �־��ش�
        playerHP.value = (float)Player.hp / Player.maxHp;   // �÷��̾� hp�� ����
        if (!bossHP.enabled)                                // ����UI ��Ȱ��ȭ �Ǿ�������
            return;                                         // �۵� ����
        bossHP.value = (float)boss.hp / boss.maxHp;         // Ȱ��ȭ �Ǿ������� ���� hp�� ����
        if(boss.hp <= 0 && !stageEnd)                       // ���� hp 0���ϵǰ� ������ ���� �ȳ�������
        {
            stageEnd = true;                                // ���� ������
            gameDirector.Invoke("ShowEnding", 3);           // ���ӵ��Ϳ��� ���� �����ִ� �޼��� 3�� �ڿ� ȣ��
        }
    }

    public void Show(Image image)
    {
        // �̹����� FadeIn �����ִ� �޼���
        StartCoroutine(FadeIn(image));
    }


    public void Hide(Image image)
    {
        // �̹����� FadeOut �����ִ� �޼���
        StartCoroutine(FadeOut(image));
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

}
