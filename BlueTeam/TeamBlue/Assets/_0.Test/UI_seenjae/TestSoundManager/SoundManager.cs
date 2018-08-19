using UnityEngine.Audio;
using UnityEngine;
using ProjectB.Utility;
using System;


namespace ProjectB.GameManager
{
    //SoundManager
    public enum SoundFXType
    {
        BGM = 0,
        ButtonClick = 1,
        PlayerAttack = 2,
        PlayerHit = 3,
        EnemyAttack = 4,
        EnemyHit = 5,
    }
    //AudioSource = Getcomponent를 해야 사용할 수 있다. 다수일 경우도 마찬가지
    //volume은 0~1까지만 컨트롤가능
    //사운드 동시재생이 되도록 
    public class SoundManager : Singleton<SoundManager>
    {
        
        
        public AudioClip[] SoundClips;
        public AudioSource[] SoundSources;
       
        
        //const int numberOfSFX = 5;
        //const int numberOfAudioSources = 2;
        [Range(0.0f,1.0f)] // 볼륨조절은 0~1사이값으로만 됨
        public float sfxVolume;
        public float bgmVolume;
        
        void Awake()
        {
            
            //sfxClips = new AudioClip[numberOfSFX];
            SoundSources = GetComponents<AudioSource>();
            //LoadSoundClips();
            //RegistSFXsource();
        }

        private void Start()
        {
            RegistBGM();
        }

        void LoadSoundClips()
        {
            //sfxClips[0] = Test_AssetBundleManager.Instance.LoadTest(BundleType.Common, "Test1");//각각 사운드 파일들 로드
        }

        //public void RegistSFXsource()
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        SoundSources[i].clip = SoundClips[i];
        //    }
           
        //}
        void PlaySound(AudioClip audioClip)
        {
            for (int i = 1; i < SoundSources.Length; i++)
            {
                if ((SoundSources[i].clip != null) && (SoundSources[i].isPlaying))
                {
                    continue;
                }
                else if ((SoundSources[i].clip != null) && (!SoundSources[i].isPlaying))
                {
                    SoundSources[i].clip = audioClip;
                    SoundSources[i].volume = sfxVolume;
                    SoundSources[i].Play();
                    break;
                }
                else if (SoundSources[i].clip == null)
                {
                    SoundSources[i].clip = audioClip;
                    SoundSources[i].volume = sfxVolume;
                    SoundSources[i].Play();
                    break;
                }
            }
        }

        public void SetSoundType(SoundFXType soundType)
        {
            AudioClip audioClip;
            switch(soundType)
            {
                case SoundFXType.ButtonClick:
                    audioClip = SoundClips[1];
                    
                    break;

                case SoundFXType.EnemyAttack:
                    audioClip = SoundClips[2];
                    break;

                case SoundFXType.EnemyHit:
                    audioClip = SoundClips[3];
                    break;

                case SoundFXType.PlayerAttack:
                    audioClip = SoundClips[4];
                    break;

                case SoundFXType.PlayerHit:
                    audioClip = SoundClips[5];
                    break;

                default:
                    audioClip = null;
                    break;
            }
            PlaySound(audioClip);
        }
        
        //BGM은 0번으로 고정시킨다.
        void RegistBGM()
        {
            SoundSources[0].clip = SoundClips[0];
        }

        public void PlayBGM()
        {
            SoundSources[0].Play();
            SoundSources[0].volume = bgmVolume;
            SoundSources[0].loop = true;
        }

        public void ControlSFXVolume()
        {
            
        }
        

        public void SetSFXVolume(float volume)
        {
            sfxVolume = volume;
        }

        public void GetSFXVolume(float volume)
        {
            bgmVolume = volume;
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
