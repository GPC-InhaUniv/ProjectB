using UnityEngine.Audio;
using UnityEngine;
using ProjectB.Utility;
using System;

namespace ProjectB.GameManager
{
    //SoundManager
    public enum SoundFXType
    {
        ButtonClick,
        PlayerAttack,
        PlayerHit,
        EnemyAttack,
        EnemyHit,

    }

    public class SoundManager : Singleton<SoundManager>
    {
        [Header("SoundClips")]
        public AudioClip[] SFXClips;
        //public AudioClip PlayerAttackClip;
        //public AudioClip PlayerHitClip;
        //public AudioClip EnemyAttackClip;
        //public AudioClip EnemyHitClip;
        //public AudioClip BGMClipClip;

        
        [HideInInspector]
        public AudioSource[] SFXSource;
        [HideInInspector]
        public AudioSource BGMSource;

        [Header("Volume")]
        
        private float SFXVolume;
       
        private float BGMVolume;

        void Awake()
        {
            //LoadSound();
            //RegistSound(SFXClips);
        }

        public void LoadSound()
        {
            //에셋번들에서 로드
        }

        //public void RegistSound(AudioClip[] audioClips)
        //{
        //    for(int i = 0; i<audioClips.Length; i++)
        //    {
        //        SFXSource[i].clip = audioClips[i];


        //    }
        //}

        public void PlayBGM(string name)
        {
            
        }

        public void PlaySFX()
        {

        }

        public AudioClip GetSoundFX(SoundFXType soundName)
        {
            //SFXVolume 적용
            AudioClip audioClip;
            switch (soundName)
            {
                case SoundFXType.ButtonClick:
                    audioClip = SFXClips[0];
                    break;

                case SoundFXType.PlayerHit:
                    audioClip = SFXClips[1];
                    break;

                case SoundFXType.PlayerAttack:
                    audioClip = SFXClips[2];
                    break;

                case SoundFXType.EnemyAttack:
                    audioClip = SFXClips[3];
                    break;

                case SoundFXType.EnemyHit:
                    audioClip = SFXClips[4];
                    break;

                default:
                    audioClip = null;
                    break;
            }
            return audioClip;
                   
        }

        //볼륨 적용은 Play()가 들어가는 시점에 해보자
        public void ReceiveBGMVolume(float bgmVolume)
        {
            BGMVolume = bgmVolume;
        }
        
        public void ReceiveSFXVolume(float sfxVolume)
        {
            SFXVolume = sfxVolume;
        }
        
    }
}
