﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 담당자 : 김정수
/// 
/// 날짜 18.07.25
/// 풀 세팅 :SetPool 호출
/// 풀에서 가져오기 : GetMonsterObject() or GetParticleObject() -> GameObject 반환
/// 풀에 반납하기 : PutObject() -> Input : GameObject  Tag : Particle을 제외한 나머지는 몬스터 풀에 저장
/// 
/// </summary>


public class Test_PoolManager : Singleton<Test_PoolManager>
{


    private void Start()
    {
      //  DontDestroyOnLoad(gameObject);
        monsterPoolSize = 20;
        FXPoolSize = 20;

    }
    enum ObjectType
    {
        monster,
        particle,
        village,
    }

    [SerializeField]
    int monsterPoolSize;

    [SerializeField]
    int FXPoolSize;

    
    List<GameObject> monster = new List<GameObject>();
    List<GameObject> particle = new List<GameObject>();

    GameObject area;
    GameObject player;
    GameObject InGamePanel;

    public GameObject MonsterPrefab;
    public GameObject ParticlePrefab;

    public void SetArea(GameObject areaObject)
    {
        area = Instantiate(areaObject);
        area.gameObject.SetActive(false);
        DontDestroyOnLoad(area);
    }

    public void SetPanel(GameObject panelObject)
    {
        InGamePanel = Instantiate(panelObject);
        InGamePanel.gameObject.SetActive(false);
        DontDestroyOnLoad(InGamePanel);
    }

    public void SetPlayer(GameObject playerobject)
    {
        player = Instantiate(playerobject);
        player.gameObject.SetActive(false);
        DontDestroyOnLoad(player);
    }

    public void SetMonster(GameObject monsterObject)
    {
        MonsterPrefab = monsterObject;
        SetPool();
      
    }


    

    public void SetPool()
    {
        for (int i = monster.Count; i < monsterPoolSize; i++)
        {
            monster.Add(CreateItem(ObjectType.monster));

        }

        for (int i = particle.Count; i < FXPoolSize; i++)
        {
            particle.Add(CreateItem(ObjectType.particle));

        }

    }

    public void ClearPool()
    {
        if(monster.Count!=0)
        monster.Clear();

        if(particle.Count!=0)
        particle.Clear();
    }

    public GameObject GetMonsterObject()
    {
        if (monster.Count == 0)
            monster.Add(CreateItem(ObjectType.monster));
        if (monster.Count > monsterPoolSize)
            return null;
        GameObject monsterObject = monster[0];
        monster.RemoveAt(0);
        monsterObject.SetActive(true);
        return monsterObject;
    }

    public GameObject GetArea()
    {
        return area;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetInGamePanel()
    {
        return InGamePanel;
    }


    public void DestroyPoolObject()
    {
        if(area!=null)
        Destroy(area);
    }

 


    public GameObject GetParticleObject()
    {
        if (monster.Count == 0)
            particle.Add(CreateItem(ObjectType.particle));
        if (monster.Count > monsterPoolSize)
            return null;
        GameObject particleObject = particle[0];
        particle.RemoveAt(0);
        particleObject.SetActive(true);
        return particleObject;
    }

    public void PutObject(GameObject gameobject)
    {

        gameobject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameobject.transform.position = new Vector3(0, 0, 0);

        if (gameobject.tag == "Particle")
            particle.Add(gameobject);
        else
            monster.Add(gameobject);

        gameobject.SetActive(false);
    }



    GameObject CreateItem(ObjectType objectType)
    {
        GameObject item;
        switch (objectType)
        {
            case ObjectType.monster:
                if (MonsterPrefab == null)
                {
                    item = null;
                    break;
                }
                item = Instantiate(MonsterPrefab);
                DontDestroyOnLoad(item);
                break;
            case ObjectType.particle:
                if (ParticlePrefab == null)
                {
                    item = null;
                    break;
                }
                item = Instantiate(ParticlePrefab);
                DontDestroyOnLoad(item);
                break;
            default:
                Debug.Log("잘못된 생성 - PoolManager");
                item = null;
                break;
        }

        if(item!=null)
        item.SetActive(false);
        return item;
    }





}
