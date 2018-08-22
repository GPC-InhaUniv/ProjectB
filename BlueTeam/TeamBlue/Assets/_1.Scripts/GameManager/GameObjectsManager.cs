using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.UI.Presenter;
namespace ProjectB.GameManager
{
    public enum ObjectType
    {
        Monster,
        Particle,
        Area,
        Player,
        Canvas,
    }
    

    public class GameObjectsManager : Singleton<GameObjectsManager>
    {
        public const int MaxMonsterCount = 18;

        GameObject areaPrefab;
        GameObject playerPrefab;
        GameObject nomalMonsterPrefab;
        GameObject namedMonsterPrefab;
        GameObject bossMonsterPrefab;
        GameObject particlePrefab;
        GameObject gameCanvasPrefab;


        [SerializeField]
        int monsterPoolSize;
        public int MonsterPoolSize { get { return monsterPoolSize; }}

        [SerializeField]
        int fxPoolSize;


        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            monsterPoolSize = MaxMonsterCount;
        }


        public void SetPrefab()
        {
            playerPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player, "PlayerCharacter");
            gameCanvasPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Common, "MainCanvas");
           
        }

        public void SetAreaPrefab(int stageNum)
        {
            int areaNum = Mathf.Abs(stageNum % 3 + 1);
            if(GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad && GameControllManager.Instance.CurrentLoadType != LoadType.Village)
            areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Stage" + areaNum.ToString());
            else
                areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Village");
        }

        public void SetMonsterPrefab()
        {
            nomalMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Normal");
            namedMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Named");
          //  bossMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "bossMonster");
        }

        GameObject areaObject;
        GameObject playerObject;
        GameObject mainUICanvas;

        public void SetObject(ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Area:
                    areaObject = Instantiate(areaPrefab);
                    DontDestroyOnLoad(areaObject);
                    break;
                case ObjectType.Player:
                    playerObject = Instantiate(playerPrefab);
                    DontDestroyOnLoad(playerObject);
                    break;
                case ObjectType.Canvas:
                    if (mainUICanvas == null)
                    {
                        mainUICanvas = Instantiate(gameCanvasPrefab);
                        DontDestroyOnLoad(mainUICanvas);
                    }
                    GameMediator.Instance.SetUICanvas();
                    break;
                default:
                    break;
            }
        }
       
        public GameObject GetAreaObject()
        {     
            return areaObject;
        }
       
        public GameObject GetPlayerObject()
        {        
            return playerObject;
        }
  
        public GameObject GetCanvasObject()
        {
            return mainUICanvas;
        }


        public void DestroyObject()
        {
            if (areaObject != null)
                Destroy(areaObject);
                      //  if (playerObject != null)
           //     Destroy(playerObject);
        }

        //Pool

        List<GameObject> monster = new List<GameObject>();
        List<GameObject> particle = new List<GameObject>();

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
        int curruntMonsterIndex = 0;
        
        public GameObject GetMonsterObject()
        {
            if (curruntMonsterIndex < monsterPoolSize)
            {
                monster[curruntMonsterIndex].SetActive(true);
                return monster[curruntMonsterIndex++];
            }
            else
                return null;
        }

        

      

    }

}