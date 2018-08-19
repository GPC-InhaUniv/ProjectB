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
    //AudioSource = Getcomponent를 해야 사용할 수 있다. 다수일 경우도 마찬가지
    //volume은 0~1까지만 컨트롤가능
    //사운드 동시재생이 되도록 
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioClip[] SoundClips; //에셋번들에서 받아온 사운드 여기에 넣어줄 것, 프라이빗으로 변경
        public AudioSource[] SoundSources;
       
        //const int numberOfSFX = 5; //에셋번들에서 받아올 SFX clips 갯수
        //const int numberOfAudioSources = 2; //이에따른 Audiosources 갯수
        
        public float sfxVolume;
        public float bgmVolume;
        
        void Awake()
        {
            sfxVolume = 1.0f;
            bgmVolume = 1.0f;
            //sfxClips = new AudioClip[numberOfSFX];
            SoundSources = GetComponents<AudioSource>();
            //LoadSoundClips();
            RegistBGM();
        }
        
        void LoadSoundClips()
        {
            //sfxClips[0] = Test_AssetBundleManager.Instance.LoadTest(BundleType.Common, "Test1");//각각 사운드 파일들 로드
        }

        void RegistBGM()//매개변수로 해당 Clip받아올것
        {
            SoundSources[0].clip = SoundClips[0];
        }

        public void PlayBGM()
        {
            SoundSources[0].Play();
            SoundSources[0].volume = bgmVolume;
            SoundSources[0].loop = true;
        }
        
        //개선 고려
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

        public void ControlVoume(float bgmVolume, float sfxVolume)
        {
            for(int i = 1; i<SoundSources.Length; i++)
            {
                SoundSources[i].volume = sfxVolume;
            }
            SoundSources[0].volume = bgmVolume;
        }
        
        //public void SetSFXVolume(float volume)
        //{
        //    sfxVolume = volume;
        //}

        //public void SetBGMVolume(float volume)
        //{
        //    bgmVolume = volume;
        //}
    }
}
