using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SoundManager : Singleton<Test_SoundManager>{

    [SerializeField]
    int countOfAudioSorce;
    GameObject bgm;
    public AudioSource[] audioSource;


    public GameObject audioPrefab;
    private void Awake()
    {
        audioSource = new AudioSource[countOfAudioSorce];
        for(int i =0; i < countOfAudioSorce; i++)
        {
            GameObject audioObject;
            audioObject = Instantiate(audioPrefab);
            audioObject.name = "AudioSorce ." + i.ToString();
            audioSource[i] = audioObject.GetComponent<AudioSource>();
        }
        bgm = Instantiate(audioPrefab); 
    }

}
