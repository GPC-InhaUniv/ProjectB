using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UsePool : MonoBehaviour {

    GameObject[] monsters = new GameObject[20];

    private void Start()
    {
        Test_PoolManager.Instance.SetPool();
    }
    int i = 0;
    public void GetMonster()
    {
       
        if (i< 20)
        {
            monsters[i] = Test_PoolManager.Instance.GetMonsterObject();
            i++;
        }
    }

    public void PutMonster()
    {
        if(i !=0)
        {
            Test_PoolManager.Instance.PutObject(monsters[i-1]);
            i--;
        }
    }
}
