using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public AudioClip SoundClip;
    AudioSource SoundSource;

    [Range(0.0f,1.0f)]
    public float Volume;
    

    public AudioClip RegistSoundClip(string SoundName)
    {
        AudioClip clip = new AudioClip();

        return clip;
    }

    public void ColtrolVolume()
    {

    }
	
}
