using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    // ���带 �����ϴ� ��ũ��Ʈ 

    public AudioClip jump;               // ���� ȿ����
    public AudioClip attack;             // ���� ȿ����
    public AudioClip shoot;              // ���Ÿ� ���� ȿ����
    public AudioClip[] sounds;           // ���� �����
    public AudioSource audioSource;      // ���� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(AudioClip audioClip)
    {
        // ���� �÷��� �޼���

        // ����� Ŭ�� ����
        audioSource.clip = audioClip;
        // ����
        audioSource.Play();
    }
    
}
