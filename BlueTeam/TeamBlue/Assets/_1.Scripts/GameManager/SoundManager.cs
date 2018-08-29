using UnityEngine;

namespace ProjectB.GameManager
{
    public enum SoundFXType
    {
        BGM,
        ButtonClick,
        PlayerAttack,
        PlayerHit,
        PlayerDeath,

        EnemyAttack,
        EnemyHit,
        EnemyExplosionSkill,
        EnemyDeath,
        BossDeath,
        Whip,

        GameOver,
        GameClear,
        ItemPickup,
        EnemyDefence
    }
   
    public class SoundManager : Singleton<SoundManager>
    {
        AudioClip[] SoundClips; 
        AudioSource[] SoundSources;
       
        const int numberOfSFX = 15;
        const int numberOfAudioSources = 6; 
        
        public float sfxVolume;
        public float bgmVolume;
        
        void Awake()
        {
            sfxVolume = 1.0f;
            bgmVolume = 1.0f;
            SoundClips = new AudioClip[numberOfSFX];
            SoundSources = new AudioSource[numberOfAudioSources];

            for (int i = 0; i < numberOfAudioSources; i++) 
            {
                SoundSources[i] = gameObject.AddComponent<AudioSource>();
            }

            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadSoundClips()
        {
            SoundClips[0] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "MainBGM");
            SoundClips[1] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "ButtonClick");
            SoundClips[2] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerAttack");
            SoundClips[3] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerHit");
            SoundClips[4] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerDeath");
            SoundClips[5] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyAttack");
            SoundClips[6] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyHit");
            SoundClips[7] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BossExplosionSkill");
            SoundClips[8] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyDeath");
            SoundClips[9] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BossDeath");
            SoundClips[10] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "GameOver");
            SoundClips[11] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "GameClear");
            SoundClips[12] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "ItemPickup");
            SoundClips[13] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "Whip");
            SoundClips[14] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyDefence");


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
                if ((SoundSources[i].clip != null) && (!SoundSources[i].isPlaying))
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
                case SoundFXType.BGM:
                    audioClip = SoundClips[1];
                    break;

                case SoundFXType.PlayerAttack:
                    audioClip = SoundClips[2];
                    break;

                case SoundFXType.PlayerHit:
                    audioClip = SoundClips[3];
                    break;

                case SoundFXType.PlayerDeath:
                    audioClip = SoundClips[4];
                    break;

                case SoundFXType.EnemyAttack:
                    audioClip = SoundClips[5];
                    break;

                case SoundFXType.EnemyHit:
                    audioClip = SoundClips[6];
                    break;

                case SoundFXType.EnemyExplosionSkill:
                    audioClip = SoundClips[7];
                    break;

                case SoundFXType.EnemyDeath:
                    audioClip = SoundClips[8];
                    break;

                case SoundFXType.BossDeath:
                    audioClip = SoundClips[9];
                    break;

                case SoundFXType.GameOver:
                    audioClip = SoundClips[10];
                    break;

                case SoundFXType.GameClear:
                    audioClip = SoundClips[11];
                    break;

                case SoundFXType.ItemPickup:
                    audioClip = SoundClips[12];
                    break;

                case SoundFXType.Whip:
                    audioClip = SoundClips[13];
                    break;

                case SoundFXType.EnemyDefence:
                    audioClip = SoundClips[14];
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
