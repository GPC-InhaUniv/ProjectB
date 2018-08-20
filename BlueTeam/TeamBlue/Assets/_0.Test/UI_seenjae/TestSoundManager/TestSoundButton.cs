using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using UnityEngine.UI;

public class TestSoundButton : MonoBehaviour
{
    [Header("임시")]
    public Button Button0;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public Button Button5;

    private void Start()
    {
        Button0.onClick.AddListener(delegate { PlayBGM(); });
        Button1.onClick.AddListener(delegate { PlaySound1(); });
        Button2.onClick.AddListener(delegate { Playsound2(); });
        Button3.onClick.AddListener(delegate { Playsound3(); });
        Button4.onClick.AddListener(delegate { PlaySound4(); });
        Button5.onClick.AddListener(delegate { PlaySound5(); });
        

    }
    public void PlayBGM()
    {
        SoundManager.Instance.PlayBGM();

    }

    public void PlaySound1()
    {
        SoundManager.Instance.SetSoundType(SoundFXType.ButtonClick);
    }

    public void Playsound2()
    {
        SoundManager.Instance.SetSoundType(SoundFXType.EnemyAttack);
    }

    public void Playsound3()
    {
        SoundManager.Instance.SetSoundType(SoundFXType.EnemyHit);
    }

    public void PlaySound4()
    {
        SoundManager.Instance.SetSoundType(SoundFXType.PlayerAttack);

    }
    public void PlaySound5()
    {
        SoundManager.Instance.SetSoundType(SoundFXType.PlayerHit);

    }





}
