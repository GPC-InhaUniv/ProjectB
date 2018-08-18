using UnityEngine.Audio;
using UnityEngine;
using ProjectB.Utility;
using System;

namespace ProjectB.GameManager
{
    //SoundManager
    public enum SoundType
    {
        ButtonClick,
        PlayerAttack,
        PlayerHit,
        EnemyAttack,
        EnemyHit,
    }
    //AudioSource = Getcomponent를 해야 사용할 수 있다. 다수일 경우도 마찬가지
    //volume은 0~1까지만 컨트롤가능
    //사운드 동시재생이 되도록 
    public class SoundManager : Singleton<SoundManager>
    {
        
        public AudioClip[] sfxClips;
        public AudioClip bgmClip;
        
        public AudioSource[] sfxSources;
        public AudioSource bgmSource;
        
        //const int numberOfSFX = 5;
        //const int numberOfAudioSources = 2;
        [Range(0.0f,1.0f)] // 볼륨조절은 0~1사이값으로만 됨
        public float sfxVolume;
        public float bgmVolume;

        void Awake()
        {
            
            //sfxClips = new AudioClip[numberOfSFX];
            sfxSources = GetComponents<AudioSource>();
            //LoadSoundClips();
        }

        private void Start()
        {
          
        }

        public void Update()
        {
            VolumeContorll();
        }
        void LoadSoundClips()
        {
            //sfxClips[0] = Test_AssetBundleManager.Instance.LoadTest(BundleType.Common, "Test1");//각각 사운드 파일들 로드
        }
      
        public void PlayBGM()
        {
            sfxSources[0].clip = sfxClips[0];
            sfxSources[0].Play();
        }
        public void PlayBGM2()
        {
            sfxSources[1].clip = sfxClips[1];
            sfxSources[1].Play();
        }
        public void PlayBGM3()
        {
            sfxSources[2].clip = sfxClips[2];
            
            sfxSources[2].Play();
            
        }

        public void PlayerBGM3()
        {
            sfxSources[3].clip = sfxClips[3];

            sfxSources[3].Play();

        }

        public void PlayerBGM4()
        {
            sfxSources[4].clip = sfxClips[4];

            sfxSources[4].Play();
        }

        public void VolumeContorll()
        {
            sfxSources[2].volume = sfxVolume;
        }
        
        

        //public void TestPlayButton()
        //{
        //    TestAudioSource.clip = bgmClip;
        //    TestAudioSource.Play();
        //}

        

        //public void LoadSoundClips()//리턴값 정하고 하는게 나으려나
        //{
        //    sfxClips[0] = Test_AssetBundleManager.Instance.LoadTest(BundleType.Common, "Test1");//각각 사운드 파일들 로드
        //    //bgmSource.clip = sfxClips[0] ;
        //}

        //public AudioClip TestRegitstSFM(AudioClip audioClip)
        //{
        //    return audioClip;
        //}




        //AudioClip[] RegistSFXSource(AudioClip[] audioClips)
        //{
        //    for(int i = 0; i<audioClips.Length; i++)
        //   {
        //        sfxSources[i].clip = audioClips[i];
        //    }
        //    return audioClips;
        //}

        //AudioClip RegistBGMSource(AudioClip audioClip)
        //{

        //    return null;
        //}

        //호출되는 메서드
        //public void PlayBGM(SoundType soundName)
        //{

        //}

        //호출되는 메서드
        //public void PlaySFX()
        //{

        //    AudioClip soundclip = SetSFXType(SoundType.ButtonClick);

        //    AudioSource SFXsource = new AudioSource();
        //    SFXsource.clip = soundclip;
        //    // source.volume =sfxVolume;
        //    SFXsource.Play();
        //}

        //public AudioClip SetSFXType(SoundType soundName)
        //{
        //    //SFXVolume 적용
        //    AudioClip audioClip;
        //    switch (soundName)
        //    {
        //        case SoundType.ButtonClick:
        //            audioClip = sfxClips[0];
        //            break;

        //        case SoundType.PlayerAttack:
        //            audioClip = sfxClips[1];
        //            break;

        //        case SoundType.PlayerHit:
        //            audioClip = sfxClips[2];
        //            break;

        //        case SoundType.EnemyAttack:
        //            audioClip = sfxClips[3];
        //            break;

        //        case SoundType.EnemyHit:
        //            audioClip = sfxClips[4];
        //            break;

        //        default:
        //            audioClip = null;
        //            break;
        //    }
        //    return audioClip;

        //}

        //볼륨 적용은 Play()가 들어가는 시점에 해보자
        //public void SetBGMVolume(float bgmVolume)
        //{
        //    this.bgmVolume = bgmVolume;

        //}

        //public void SetSFXVolume(float sfxVolume)
        //{
        //    this.sfxVolume = sfxVolume;
        //}

    }
}
