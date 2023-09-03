using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    // 사운드를 관리하는 스크립트 

    public AudioClip jump;               // 점프 효과음
    public AudioClip attack;             // 공격 효과음
    public AudioClip shoot;              // 원거리 공격 효과음
    public AudioClip[] sounds;           // 각종 사운드들
    public AudioSource audioSource;      // 사운드 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(AudioClip audioClip)
    {
        // 사운드 플레이 메서드

        // 오디오 클립 세팅
        audioSource.clip = audioClip;
        // 실행
        audioSource.Play();
    }
    
}
