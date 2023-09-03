using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    // �������� �� ���� ��ũ��Ʈ
    // �������� ���� �� ������Ʈ�� ����� ��ũ��Ʈ
    // �������� ���� ���� ������Ʈ�� ����� ��ũ��Ʈ

    public GameObject map;                 // ���� ������Ʈ
    public GameObject mapInfo;             // ���� �Ѷ�� �˷��ִ� ������Ʈ
    public GameObject home;                // �浹 ó���� �� ������Ʈ

    // ������Ʈ �ҷ�����
    public StageDirector stageDirector;    
    public StoryDirector storyDirector;    
    public BoxCollider2D homeCollider;     

    // Start is called before the first frame update
    void Start()
    {
        // �� ������������ ������Ʈ�� �ִ� ������Ʈ �ҷ�����
        stageDirector = GameObject.Find("StageDirector").GetComponent<StageDirector>();
        
        // �������� ������Ʈ�� �������� ������Ʈ �ҷ�����
        if(gameObject.name == "StageDirector")
        {
            storyDirector = GetComponent<StoryDirector>();
            // ���� ȭ�� ���̵� �ƿ� ȿ��
            storyDirector.Hide(storyDirector.black);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �Ʒ� ȭ��ǥ ������ 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ���� ������Ʈ ��Ȱ��ȭ
            map.SetActive(false);

            // �� �ݶ��̴� Ȱ��ȭ�ؼ� �ٽ� ���� Ȱ��ȭ ��ų �� �ְ� �ϱ�
            stageDirector.homeCollider.enabled = true;
        }
    }

    private void OnMouseEnter()
    {
        // ������ ��, ����� ��, ���� ���� ���콺�� ������ ��� ũ�� Ű��� ȿ��
        if (gameObject.name == "map_monkey" || gameObject.name == "map_boar" || gameObject.name == "map_lion")
            transform.localScale = Vector2.one * 1.05f;
    }
    private void OnMouseExit()
    {
        // ������ ��, ����� ��, ���� ������ ���콺�� ���� ���� ũ�� ��ȯ
        if (gameObject.name == "map_monkey" || gameObject.name == "map_boar" || gameObject.name == "map_lion")
            transform.localScale = Vector2.one * 1.00f;
    }

    private IEnumerator OnMouseDown()
    {
        // ������ �� ���� Ŭ���ϸ�
        // show �޼��� ȣ��� ���� ȭ�� FadeIn ȿ��
        // 1.5�� �� �ش� �������� �̵�
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
        // ���� ���˵� ���·�
        if (Input.GetKeyDown(KeyCode.UpArrow))            // �� ����Ű�� ������
        {
            map.SetActive(true);                          // ���� Ȱ��ȭ
            stageDirector.homeCollider.enabled = false;   // �� ������Ʈ �ݶ��̴� ����(���� ũ�� ��ȭ �۵��� ������ �༭ ������)
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �����ϸ� ���� Ȱ��ȭ ���� ��������Ʈ Ȱ��ȭ
        stageDirector.mapInfo.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� �������� ���� Ȱ��ȭ ���� ��������Ʈ ��Ȱ��ȭ
        stageDirector.mapInfo.SetActive(false);

    }
}
