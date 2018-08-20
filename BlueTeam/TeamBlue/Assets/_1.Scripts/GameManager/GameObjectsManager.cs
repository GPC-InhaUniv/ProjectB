using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.GameManager
{

    public class GameObjectsManager : Singleton<GameObjectsManager>
    {
        GameObject areaPrefab;
        GameObject playerPrefab;
        GameObject nomalMonsterPrefab;
        GameObject namedMonsterPrefab;
        GameObject bossMonsterPrefab;
        GameObject particlePrefab;
        GameObject gameCanvasPrefab;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        public void SetPrefab()
        {
            playerPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player, "PlayerCharacter");
          //  gameCanvasPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Common, "MainCanvas");
        }

        public void SetAreaPrefab(int stageNum)
        {
            int areaNum = Mathf.Abs(stageNum % 3 + 1);
            if(GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad)
            areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Stage" + areaNum.ToString());
            else
                areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Village");
        }

        public void SetMonsterPrefab()
        {
            nomalMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "nomalMonster");
            namedMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "nameMonster");
            bossMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "bossMonster");
        }

        GameObject areaObject;
        GameObject playerObject;

        public GameObject GetAreaObject()
        {
            areaObject = Instantiate(areaPrefab);
            return areaObject;
        }
        public GameObject GetPlayerObject()
        {
            playerObject = Instantiate(playerPrefab);
            return playerObject;
        }
        public void DestroyObject()
        {
            if (areaObject != null)
                Destroy(areaObject);
            if (playerObject != null)
                Destroy(playerObject);
        }

        //Pool

        List<GameObject> monster = new List<GameObject>();
        List<GameObject> particle = new List<GameObject>();

        [SerializeField]
        int monsterPoolSize;

        [SerializeField]
        int fxPoolSize;


        enum ObjectType
        {
            Monster,
            Particle,
            Area,
            Player,
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
        GameObject CreateItem(ObjectType objectType)
        {
            GameObject item;
            switch (objectType)
            {
                case ObjectType.Monster:
                    item = Instantiate(nomalMonsterPrefab);
                    DontDestroyOnLoad(item);
                    break;
                case ObjectType.Particle:
                    item = Instantiate(particlePrefab);
                    DontDestroyOnLoad(item);
                    break;
                case ObjectType.Player:
                    item = Instantiate(playerPrefab);
                    DontDestroyOnLoad(item);
                    break;
                case ObjectType.Area:
                    item = Instantiate(areaPrefab);
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

    }

}