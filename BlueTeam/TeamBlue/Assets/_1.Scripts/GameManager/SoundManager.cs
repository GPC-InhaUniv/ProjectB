using UnityEngine;

namespace ProjectB.GameManager
{
    public enum SoundFXType
    {
        BGM,
        ButtonClick,
        PlayerAttack,
        PlayerHit,
        EnemyAttack,
        EnemyHit,
    }
   
    public class SoundManager : Singleton<SoundManager>
    {
        AudioClip[] SoundClips; 
        AudioSource[] SoundSources;
       
        const int numberOfSFX = 6;
        const int numberOfAudioSources = 6; 
        
        public float sfxVolume;
        public float bgmVolume;
        
        void Awake()
        {
            sfxVolume = 1.0f;
            bgmVolume = 1.0f;
            SoundClips = new AudioClip[numberOfSFX];
            SoundSources = GetComponents<AudioSource>();
        }
        
        public void LoadSoundClips()
        {
            SoundClips[0] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "MainBGM");
            SoundClips[1] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "ButtonClick");
            SoundClips[2] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerAttack");
            SoundClips[3] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerHit");
            SoundClips[4] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyAttack");
            SoundClips[5] = Test_AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyHit");

            SoundSources[0].clip = SoundClips[0];
        }
        
        public void PlayBGM()
        {
            SoundSources[0].Play();
            SoundSources[0].volume = bgmVolume;
            SoundSources[0].loop = true;
        }
        
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

        public void SetSound(SoundFXType soundType)
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

        public void SetVolume(float bgmVolume, float sfxVolume)
        {
            for (int i = 1; i < SoundSources.Length; i++) 
            {
                SoundSources[i].volume = sfxVolume;
            }
            SoundSources[0].volume = bgmVolume;
        }
    }
}
