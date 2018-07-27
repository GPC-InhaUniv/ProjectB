using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test_SceneManager : MonoBehaviour {


    public string SceneName;

    public void ChangeNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }

}
