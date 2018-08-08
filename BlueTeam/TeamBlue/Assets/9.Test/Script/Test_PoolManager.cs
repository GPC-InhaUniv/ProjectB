using System.Collections.Generic;
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
        DontDestroyOnLoad(gameObject);
    }
    enum ObjectType
    {
        monster,
        particle,
    }

    [SerializeField]
    int monsterPoolSize;

    [SerializeField]
    int FXPoolSize;

    List<GameObject> monster = new List<GameObject>();
    List<GameObject> particle = new List<GameObject>();

    public GameObject monsterPrefab;
    public GameObject particlePrefab;

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
        monster.Clear();
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
                item = Instantiate(monsterPrefab);
                DontDestroyOnLoad(item);
                break;
            case ObjectType.particle:
                item = Instantiate(particlePrefab);
                DontDestroyOnLoad(item);
                break;
            default:
                Debug.Log("잘못된 생성 - PoolManager");
                item = null;
                break;
        }
        item.SetActive(false);
        return item;
    }





}
