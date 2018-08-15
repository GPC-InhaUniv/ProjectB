using UnityEngine.Audio;
using UnityEngine;
using ProjectB.Utility;

namespace ProjectB.GameManager
{
    //SoundManager
    public class SoundManager : Singleton<SoundManager>
    {
        public Sound[] sounds;

        public void PlaySound(string soundName)
        {
            Debug.Log("재생됨");
        }
    }
}
