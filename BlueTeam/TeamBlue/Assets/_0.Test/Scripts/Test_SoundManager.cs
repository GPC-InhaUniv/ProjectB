using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 담당자 : 김정수
/// 
/// 날짜 : 180726
/// 사운드 사용시 사운드 타입 입력 필수
/// 모든 사운드 사용시 재생 안됨
/// 에셋번들에서 로드 해야함
/// </summary>


public enum SoundType
{
    Attack,
    Hit,
    Groan,
}

public class Test_SoundManager : Singleton<Test_SoundManager>{

    [SerializeField]
    int countOfAudioSource;
    GameObject bgmObject;
   
    public AudioSource[] audioSource;
    AudioSource bgm;

    public GameObject audioPrefab;

    AudioClip bgmAudioClip;
    AudioClip attackAudioClip;
    AudioClip hitAudioClip;
    AudioClip groanAudioClip;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SetSoundManager();
    }

    void SetSoundManager()
    {
        audioSource = new AudioSource[countOfAudioSource];
        for (int i = 0; i < countOfAudioSource; i++)
        {
            GameObject audioObject;
            audioObject = Instantiate(audioPrefab);
            audioObject.name = "AudioSource. " + i.ToString();
            audioSource[i] = audioObject.GetComponent<AudioSource>();
        }
        bgmObject = Instantiate(audioPrefab);
        bgmObject.name = "BGM";
        bgm = bgmObject.GetComponent<AudioSource>();
    }

    public void LoadAudioClip()
    {
        //에셋번들에서 오디오 클립 로드필요
    }

    public AudioClip GetAudioClip(SoundType soundType)
    {
        AudioClip clip;
        switch (soundType)
        {
            case SoundType.Attack:
                clip = attackAudioClip;
                break;
            case SoundType.Hit:
                clip = hitAudioClip;
                break;
            case SoundType.Groan:
                clip = groanAudioClip;
                break;
            default:
                clip = null;
                break;
        }
        return clip;
    }

    public void StartBGM()
    {
        bgm.loop = true;
        bgm.Play();
    }

    public void StartSound(SoundType soundType)
    {
        foreach(AudioSource audio in audioSource)
        {
            if(!audio.isPlaying)
            {
                audio.clip = GetAudioClip(soundType);
                audio.Play();
                return;
            }
        }
    }
}
