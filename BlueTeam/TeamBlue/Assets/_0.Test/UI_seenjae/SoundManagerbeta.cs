using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerbeta : Singleton<SoundManagerbeta>
{
    List<Sound> sounds;
    List<AudioSource> audioSources;

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        sounds = new List<Sound>();

    }

    

    float BGMVolume;
    float SFXVolume;
	
}
