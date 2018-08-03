using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObjectProduction : MonoBehaviour {

    public Image DotImage;
    public Radar radar;

    void Start()
    {
        radar.RegistEnemyIcon(this.gameObject, DotImage);
    }
}
