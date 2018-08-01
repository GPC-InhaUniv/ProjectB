using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MakeRaderObject : MonoBehaviour
{
    public Image image;
    public Rader rader;

    //private void Start()
    //{
    //    Rader.RegisterRaderObject(this.gameObject, image);  
    //}

    //private void Update()
    //{
       
    //}

    private void OnEnable()
    {
        rader.RegisterRaderObject(this.gameObject, image);
    }

    private void OnDisable()
    {
        rader.RemoveRaderObject(this.gameObject);
    }
    //private void OnDestroy()
    //{
    //    Rader.RemoveRaderObject(this.gameObject);
    //}
}
