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
    
    public class SoundManager : Singleton<SoundManager>
    {
        
        public AudioClip[] sfxClips;
        public AudioClip bgmClip;
        
        public AudioSource[] sfxSources;
        public AudioSource bgmSource;

        int numberOfSFX;
        public float sfxVolume;
        public float bgmVolume;

        void Awake()
        {
            numberOfSFX = 5;
            sfxClips = new AudioClip[numberOfSFX];
            sfxSources = new AudioSource[numberOfSFX];
            LoadSoundClips();
            //RegistSFXSource(sfxClips);
            //RegistSound(SFXClips);
        }

        void LoadSoundClips()//리턴값 정하고 하는게 나으려나
        {
            //추가하는 것 수정/추가하기 좋게 하려면 ??
            sfxClips[0] = Test_AssetBundleManager.Instance.LoadTest(BundleType.Common, "Test1");//각각 사운드 파일들 로드

        }

        AudioClip[] RegistSFXSource(AudioClip[] audioClips)
        {
            for(int i = 0; i<audioClips.Length; i++)
           {
                sfxSources[i].clip = audioClips[i];
            }
            return audioClips;
        }

        //AudioClip RegistBGMSource(AudioClip audioClip)
        //{

        //    return null;
        //}

        //호출되는 메서드
        public void PlayBGM(SoundType soundName)
        {
            
        }

        //호출되는 메서드
        public void PlaySFX()
        {
            
            AudioClip soundclip = SetSFXType(SoundType.ButtonClick);
            
            AudioSource SFXsource = new AudioSource();
            SFXsource.clip = soundclip;
            // source.volume =sfxVolume;
            SFXsource.Play();
        }

        public AudioClip SetSFXType(SoundType soundName)
        {
            //SFXVolume 적용
            AudioClip audioClip;
            switch (soundName)
            {
                case SoundType.ButtonClick:
                    audioClip = sfxClips[0];
                    break;

                case SoundType.PlayerAttack:
                    audioClip = sfxClips[1];
                    break;

                case SoundType.PlayerHit:
                    audioClip = sfxClips[2];
                    break;

                case SoundType.EnemyAttack:
                    audioClip = sfxClips[3];
                    break;

                case SoundType.EnemyHit:
                    audioClip = sfxClips[4];
                    break;

                default:
                    audioClip = null;
                    break;
            }
            return audioClip;
                   
        }

        //볼륨 적용은 Play()가 들어가는 시점에 해보자
        public void SetBGMVolume(float bgmVolume)
        {
            this.bgmVolume = bgmVolume;
            
        }
        
        public void SetSFXVolume(float sfxVolume)
        {
            this.sfxVolume = sfxVolume;
        }
        
    }
}
