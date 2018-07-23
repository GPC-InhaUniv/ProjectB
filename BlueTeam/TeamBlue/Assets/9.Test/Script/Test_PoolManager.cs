using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MonsterObject
{
    public GameObject monster;
    public int index;
}
public class Test_PoolManager : Singleton<Test_PoolManager> {

    MonsterObject[] monsterPool = new MonsterObject[20];
    Queue unUsedMonsterNumber = new Queue();

    public GameObject monsterPrefab;

    private void Awake()
    {   
        for(int i = 0; i< monsterPool.Length;i++)
        {
            monsterPool[i].monster = Instantiate(monsterPrefab);
            monsterPool[i].index = i;
            monsterPool[i].monster.SetActive(false);
            unUsedMonsterNumber.Enqueue(i);  
        }
    }

   
    
}
