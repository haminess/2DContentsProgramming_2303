using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    // ���� �� ���� ��ũ��Ʈ

    // ������Ʈ �ҷ�����
    StoryDirector storyDirector;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        storyDirector = GetComponent<StoryDirector>();

        // ���� ���丮 ��� �޼��� ȣ��
        StartCoroutine(storyDirector.ShowStartStory());
    }

    // Update is called once per frame
    void Update()
    {
        // ���丮�� ����� ������
        if (!storyDirector.isPlaying && Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���ϸ� �������� ������ �̵�
            SceneManager.LoadScene("StageScene");
        }
    }
}
