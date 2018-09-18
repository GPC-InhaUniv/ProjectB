using UnityEngine;

namespace ProjectB.GameManager
{
    public enum SoundFXType
    {
        BGM=0,
        ButtonClick=1,
        PlayerAttack=2,
        PlayerHit=3,
        PlayerDeath=4,

        EnemyAttack=5,
        EnemyHit=6,
        EnemyExplosionSkill=7,
        EnemyDeath=8,
        BossDeath=9,
        Whip=10,

        GameOver=11,
        GameClear=12,
        ItemPickup=13,
        EnemyDefence=14
    }

    public enum BGMType
    {
        Village=0,
        BricksDungeon=1,
        IronDungeon=2,
        WoodDungeon=3,
        SheepDungeon=4,
        BossDungeon=5
    }
   
    public class SoundManager : Singleton<SoundManager>
    {
        AudioClip[] SFXClips;
        AudioClip[] BGMClips;
        AudioSource[] SoundSources;
       
        const int numberOfSFX = 15;
        const int numberOfBGM = 6;
        const int numberOfAudioSources = 6; 
        
        public float sfxVolume;
        public float bgmVolume;
        
        void Awake()
        {
            sfxVolume = 1.0f;
            bgmVolume = 1.0f;
            SFXClips = new AudioClip[numberOfSFX];
            BGMClips = new AudioClip[numberOfBGM];
            SoundSources = new AudioSource[numberOfAudioSources];

            for (int i = 0; i < numberOfAudioSources; i++) 
            {
                SoundSources[i] = gameObject.AddComponent<AudioSource>();
            }

            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadSoundClips()
        {
            SFXClips[0] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "MainBGM");
            SFXClips[1] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "ButtonClick");
            SFXClips[2] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerAttack");
            SFXClips[3] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerHit");
            SFXClips[4] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "PlayerDeath");
            SFXClips[5] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyAttack");
            SFXClips[6] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyHit");
            SFXClips[7] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BossExplosionSkill");
            SFXClips[8] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyDeath");
            SFXClips[9] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BossDeath");
            SFXClips[10] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "GameOver");
            SFXClips[11] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "GameClear");
            SFXClips[12] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "ItemPickup");
            SFXClips[13] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "Whip");
            SFXClips[14] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "EnemyDefence");

            BGMClips[0] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "MainBGM");
            BGMClips[1] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BrickDungeon");
            BGMClips[2] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "IronDungeon");
            BGMClips[3] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "WoodDungeon");
            BGMClips[4] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "SheepDugeon");
            BGMClips[5] = AssetBundleManager.Instance.LoadSound(BundleType.Common, "BossDungeon");

        }



        public void SetBGM(BGMType bgmType)
        {
            AudioClip bgmClip;
            bgmClip = BGMClips[(int)bgmType];

            PlayBGM(bgmClip);
        }

        public void PlayBGM(AudioClip bgmClip)
        {
            SoundSources[0].clip = bgmClip;
            SoundSources[0].Play();
            SoundSources[0].volume = bgmVolume;
            SoundSources[0].loop = true;
        }

        public void SetSFX(SoundFXType soundType)
        {
            AudioClip audioClip;
            audioClip = SFXClips[(int)soundType];
            PlaySFX(audioClip);
        }

        //오버로드 시켜서 쓰면 될듯 싶다. 그런데 여기에 오버로드가지 써야하나..?
        public void SetAudio()
        {

        }
        



        //본래의 사운드 매니저 파츠
        public void PlayBGM()
        {
            SoundSources[0].clip = SFXClips[0];
            SoundSources[0].Play();
            SoundSources[0].volume = bgmVolume;
            SoundSources[0].loop = true;
        }
        
        void PlaySFX(AudioClip audioClip)
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
        
        public void SetVolume(float bgmVolume, float sfxVolume)
        {
            for (int i = 1; i < SoundSources.Length; i++) 
            {
                SoundSources[i].volume = sfxVolume;
            }
            SoundSources[0].volume = bgmVolume;
        }


        public void SetSound(SoundFXType soundType)
        {
            AudioClip audioClip;
            switch (soundType)
            {
                case SoundFXType.BGM:
                    audioClip = SFXClips[1];
                    break;

                case SoundFXType.PlayerAttack:
                    audioClip = SFXClips[2];
                    break;

                case SoundFXType.PlayerHit:
                    audioClip = SFXClips[3];
                    break;

                case SoundFXType.PlayerDeath:
                    audioClip = SFXClips[4];
                    break;

                case SoundFXType.EnemyAttack:
                    audioClip = SFXClips[5];
                    break;

                case SoundFXType.EnemyHit:
                    audioClip = SFXClips[6];
                    break;

                case SoundFXType.EnemyExplosionSkill:
                    audioClip = SFXClips[7];
                    break;

                case SoundFXType.EnemyDeath:
                    audioClip = SFXClips[8];
                    break;

                case SoundFXType.BossDeath:
                    audioClip = SFXClips[9];
                    break;

                case SoundFXType.GameOver:
                    audioClip = SFXClips[10];
                    break;

                case SoundFXType.GameClear:
                    audioClip = SFXClips[11];
                    break;

                case SoundFXType.ItemPickup:
                    audioClip = SFXClips[12];
                    break;

                case SoundFXType.Whip:
                    audioClip = SFXClips[13];
                    break;

                case SoundFXType.EnemyDefence:
                    audioClip = SFXClips[14];
                    break;

                default:
                    audioClip = null;
                    break;
            }
            PlaySFX(audioClip);
        }
    }
}
