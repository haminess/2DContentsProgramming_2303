using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // �������� ���� ���� ��ũ��Ʈ
    
    // ������Ʈ �ҷ�����
    StoryDirector storyDirector;

    // ���� ����
    public static bool[] stageStory = { false, false, false, false };      // �������� ���� ���丮�� �þ����� üũ, �� ���丮�� �ٽ� ���� �ʴ´�
    public bool[] stageStory_show;                                         // �ν����� â���� ���� �뵵
    public int stageNum;                                                   // �������� ��ȣ
    public GameObject player;                                              // �÷��̾� ������Ʈ
    public GameObject boss;                                                // ���� ������Ʈ
    public GameObject camera0;                                             // ���� ī�޶� ������Ʈ

    

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        storyDirector = GetComponent<StoryDirector>();

        Player.hp = Player.maxHp;      // �÷��̾� ü�� ä���

        // ���� ����
        if (!stageStory[stageNum])
        {
            // ���丮 �Ⱥ����� ���丮 ���
            ShowStartStory();
        }
        else
        {
            // ���丮 ������ �ٷ� ���� ����
            storyDirector.HideBlack();    // ���� ȭ�鿡�� ���������� FadeIn ȿ��
            StartStage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �ν����� â���� �� �� �ֵ��� ��ġ ����
        stageStory_show = stageStory;
    }

    // ���丮 ��� �޼���
    void ShowStartStory()
    {
        // �������� �� ���丮 ��� �޼���
        switch (stageNum)
        {
            case 1:
                StartCoroutine(storyDirector.ShowStage1Story());     // 1���������� ���丮1 ���
                stageStory[stageNum] = true;
                break;
            case 2:
                StartCoroutine(storyDirector.ShowStage2Story());     // 2���������� ���丮2 ���
                stageStory[stageNum] = true;
                break;
            case 3:
                StartCoroutine(storyDirector.ShowStage3Story());     // 3���������� ���丮3 ���
                stageStory[stageNum] = true;                         // ���� ���丮 ���� ���� üũ
                break;
        }
    }
    // ���� ��� �޼���
    public void ShowEnding()
    {
        // �������� �� ���� ��� �޼���
        switch (stageNum)
        {
            case 1:
                StartCoroutine(storyDirector.ShowStage1Ending());    // 1�������� ���� ���
                break;
            case 2:
                StartCoroutine(storyDirector.ShowStage2Ending());    // 2�������� ���� ���
                break;
            case 3:
                StartCoroutine(storyDirector.ShowGameEnding());    // 3�������� ���� ���
                break;
        }
    }
    // ���� ���� �޼���
    public void StartStage()
    {
        // �� ���� ���� �޼���
        player.SetActive(true);        // �÷��̾� Ȱ��ȭ
        if (!boss.activeSelf)          // ���� ��Ȱ��ȭ �Ǿ�������
        {
            boss.SetActive(true);      // ���� Ȱ��ȭ
        }                              // 1��������������
        if(stageNum == 1)
        {
            camera0.transform.SetParent(player.transform);    // ī�޶� �÷��̾ ���󰡵��� ����
        }
    }
    // ���� Ż�� �޼���
    public IEnumerator ExitStage()
    {
        // ���������� ���ư��� �޼���, �ڷ�ƾ �Լ��� IEnumerator ��ȯ
        yield return new WaitForSeconds(3);        // 3�� ��
        storyDirector.ShowBlack();                 // ���� ȭ�� FadeOut ȿ����
        yield return new WaitForSeconds(1);        // 1�� ��
        SceneManager.LoadScene("StageScene");      // �������� ������ ��ȯ
    }
    // �߶� �� �۵� �޼���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            // �÷��̾ �ٴڰ� �浹���� ��
            // ü�� ����
            Player.hp -= 10;
            // ������ �ٽ� ��ȯ
            player.transform.position = new Vector2(player.transform.position.x, 20);

        }
    }
}
