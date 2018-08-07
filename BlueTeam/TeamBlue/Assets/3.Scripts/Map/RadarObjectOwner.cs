using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObjectOwner : MonoBehaviour {

    public Image IconImage;
    public Radar radar;

    void Start()
    {
        radar.RegistIcon(this.gameObject, IconImage);
    }

    private void Update()
    {
        
    }
}
