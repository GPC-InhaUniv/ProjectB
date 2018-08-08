using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Return : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickBtn()
    {
        LoadingSceneManager.LoadScene(LoadType.Village, 0);
       
    }
}
