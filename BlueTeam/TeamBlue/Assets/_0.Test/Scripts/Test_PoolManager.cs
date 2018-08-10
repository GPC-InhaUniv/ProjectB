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
        //  DontDestroyOnLoad(gameObject);
        monsterPoolSize = 20;
        fxPoolSize = 20;
    }
    enum ObjectType
    {
        Monster,
        Particle,
        Area,
        Player,

    }

    [SerializeField]
    int monsterPoolSize;

    [SerializeField]
    int fxPoolSize;


    List<GameObject> monster = new List<GameObject>();
    List<GameObject> particle = new List<GameObject>();

    GameObject player;
    GameObject area;

    public GameObject AreaPrefab;
    public GameObject PlayerPrefab;
    public GameObject MonsterPrefab;
    public GameObject ParticlePrefab;


    public void SetArea(GameObject areaObject)
    {
        AreaPrefab = areaObject;
        AreaPrefab.gameObject.SetActive(false);
        DontDestroyOnLoad(AreaPrefab);
    }


    public void SetPlayer(GameObject playerobject)
    {
        PlayerPrefab = Instantiate(playerobject);
        PlayerPrefab.gameObject.SetActive(false);
        DontDestroyOnLoad(PlayerPrefab);
    }



    public void SetPool()
    {
        for (int i = monster.Count; i < monsterPoolSize; i++)
        {
            monster.Add(CreateItem(ObjectType.Monster));

        }
        for (int i = particle.Count; i < fxPoolSize; i++)
        {
            particle.Add(CreateItem(ObjectType.Particle));
        }
        //    player = CreateItem(ObjectType.Player);
        //   area = CreateItem(ObjectType.Area);
    }

    public void ClearPool()
    {
        for (int i = 0; i < monster.Count; i++)
        {
            Destroy(monster[i]);
        }

        for (int i = 0; i < particle.Count; i++)
        {
            Destroy(particle[i]);
        }
        monster.Clear();
        particle.Clear();
    }
    public void DestroyArea()
    {
        if (area != null)
            Destroy(area);
    }
    public GameObject GetMonsterObject()
    {
        if (monster.Count == 0)
            monster.Add(CreateItem(ObjectType.Monster));
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


    public void DestroyPoolObject()
    {
        if (area != null)
            Destroy(area);
    }




    public GameObject GetParticleObject()
    {
        if (monster.Count == 0)
            particle.Add(CreateItem(ObjectType.Particle));
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
            case ObjectType.Monster:
                item = Instantiate(MonsterPrefab);
                DontDestroyOnLoad(item);
                break;
            case ObjectType.Particle:
                item = Instantiate(ParticlePrefab);
                DontDestroyOnLoad(item);
                break;
            case ObjectType.Player:
                item = Instantiate(PlayerPrefab);
                DontDestroyOnLoad(item);
                break;
            case ObjectType.Area:
                item = Instantiate(AreaPrefab);
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
