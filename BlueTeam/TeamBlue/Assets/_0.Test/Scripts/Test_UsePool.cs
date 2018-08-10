using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

public class Test_UsePool : MonoBehaviour {

    GameObject test;
    public void SetPool()
    {
        test = GameObjectsManager.Instance.GetAreaObject();
    }

 
}
