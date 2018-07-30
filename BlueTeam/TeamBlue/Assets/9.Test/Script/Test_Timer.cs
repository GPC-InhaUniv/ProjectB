using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Timer : MonoBehaviour {
   
    public float deltaTime;

    private void Update()
    {
        deltaTime = Time.deltaTime;
    }
}
