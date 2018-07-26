using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 담당자 : 김정수
/// 
/// 날짜 18.07.25
/// 풀 세팅 : SetPool 호출
/// 풀에서 가져오기 : GetMonsterObject() or GetParticleObject() -> GameObject 반환
/// 풀에 반납하기 : PutObject() -> Input : GameObject  Tag : Particle을 제외한 나머지는 몬스터 풀에 저장
/// 
/// Inspector 에서 풀 사이즈 조정
/// 
/// Prefab은 윤우님 작업이 진행되면 조정 예정
/// </summary>

public class PoolManager : Singleton<PoolManager> {

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

    List<GameObject> monsterPool = new List<GameObject>();
    List<GameObject> particlePool = new List<GameObject>();

    public GameObject monsterPrefab;
    public GameObject particlePrefab;

    public void SetPool()
    {
        for (int i = monsterPool.Count; i < monsterPoolSize; i++)
        {
            monsterPool.Add(CreateItem(ObjectType.monster));

        }

        for (int i = particlePool.Count; i < FXPoolSize; i++)
        {
            particlePool.Add(CreateItem(ObjectType.particle));

        }
    }

    public void ClearPool()
    {
        monsterPool.Clear();
        particlePool.Clear();
    }

    public GameObject GetMonsterObject()
    {
        if (monsterPool.Count == 0)
            monsterPool.Add(CreateItem(ObjectType.monster));
        if (monsterPool.Count > monsterPoolSize)
            return null;
        GameObject monsterObject = monsterPool[0];
        monsterPool.RemoveAt(0);
        monsterObject.SetActive(true);
        return monsterObject;
    }


    public GameObject GetParticleObject()
    {
        if (particlePool.Count == 0)
            particlePool.Add(CreateItem(ObjectType.particle));
        if (particlePool.Count > monsterPoolSize)
            return null;
        GameObject particleObject = particlePool[0];
        particlePool.RemoveAt(0);
        particleObject.SetActive(true);
        return particleObject;
    }
    public void PutObject(GameObject gameobject)
    {

        gameobject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameobject.transform.position = new Vector3(0, 0, 0);

        if (gameobject.tag == "Particle")
            particlePool.Add(gameobject);
        else
            monsterPool.Add(gameobject);

        gameobject.SetActive(false);
    }



    GameObject CreateItem(ObjectType objectType)
    {
        GameObject item;
        switch (objectType)
        {
            case ObjectType.monster:
                item = Instantiate(monsterPrefab);
                break;
            case ObjectType.particle:
                item = Instantiate(particlePrefab);
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
