using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

public class Test_UsePool : MonoBehaviour {

    GameObject test;
    public void SetPool()
    {
        Test_AssetBundleManager.Instance.LoadArea(AreaType.Village);
        Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Village");
    }

    public void testinstance()
    {
        GameObjectsManager.Instance.SetAreaPrefab(1);
    }
 
    public void Instancing()
    {
        test = GameObjectsManager.Instance.GetAreaObject();
        test.transform.position = new Vector3(0, 0, 0);
    }
}
