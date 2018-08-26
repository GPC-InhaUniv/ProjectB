using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.UI.Presenter;
using ProjectB.Characters.Monsters;

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
        GameObject[] bossSkill;
        GameObject gameCanvasPrefab;


        [SerializeField]
        int monsterPoolSize;
        public int MonsterPoolSize { get { return monsterPoolSize; } }

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
            if (GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad && GameControllManager.Instance.CurrentLoadType != LoadType.Village)
                areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Stage" + areaNum.ToString());
            else
                areaPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Village");
        }

        public void SetMonsterPrefab()
        {
            nomalMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Normal");
            namedMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "Named");
            if (GameControllManager.Instance.CurrentLoadType == LoadType.BossDungeon)
            {
                bossMonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "bossMonster");
                bossSkill[0] = Instantiate(Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "BossSkill1"));
                bossSkill[1] = Instantiate(Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "BossSkill2"));
                bossSkill[2] = Instantiate(Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "BossSkill3"));
            }

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

        GameObject[] normalMonster;
        GameObject[] namedMonster;
        GameObject bossMonster;

        int curruntNormalMonsterIndex = 0;
        int curruntNamedMonsterIndex = 0;

        public void SetPool()
        {
            normalMonster = new GameObject[monsterPoolSize];
            namedMonster = new GameObject[monsterPoolSize / 3];
            for (int i = 0; i < normalMonster.Length; i++)
            {
                normalMonster[i] = Instantiate(nomalMonsterPrefab);
                DontDestroyOnLoad(normalMonster[i]);
                normalMonster[i].SetActive(false);
            }
            for (int i = 0; i < namedMonster.Length; i++)
            {
                namedMonster[i] = Instantiate(namedMonsterPrefab);
                DontDestroyOnLoad(namedMonster[i]);
                normalMonster[i].SetActive(false);
            }
            //bossMonster = Instantiate(bossMonsterPrefab);
           // bossMonster.SetActive(false);

        }

        public void ClearPool()
        {/*
            if (normalMonster == null)
                return;
            for (int i = 0; i < normalMonster.Length; i++)
            {
                Destroy(normalMonster[i]);
            }
            for (int i = 0; i < namedMonster.Length; i++)
            {
                Destroy(namedMonster[i]);
            }
            
            for (int i = 0; i < bossSkill.Length; i++)
            {
                Destroy(bossSkill[i]);
            }
          if(bossMonster !=null)
                Destroy(bossMonster);*/
            curruntNormalMonsterIndex = 0;
            curruntNamedMonsterIndex = 0;
        }

        

        public GameObject GetMonsterObject(MonsterType monsterType)
        {
            GameObject monsterObject;
            switch (monsterType)
            {
                case MonsterType.Normal:
                    if (curruntNormalMonsterIndex < normalMonster.Length)
                    {
                        normalMonster[curruntNormalMonsterIndex].SetActive(true);
                        monsterObject = normalMonster[curruntNormalMonsterIndex++];
                    }
                    else
                        monsterObject = null;
                    break;
                case MonsterType.Named:
                    if (curruntNamedMonsterIndex < normalMonster.Length)
                    {
                        namedMonster[curruntNamedMonsterIndex].SetActive(true);
                        monsterObject = namedMonster[curruntNamedMonsterIndex++];
                    }
                    else
                        monsterObject = null;
                    break;
                case MonsterType.Boss:
                    monsterObject = bossMonster;
                    break;
                default:
                    monsterObject = null;
                    break;
            }

            return monsterObject;
           
        }

        public GameObject BossSkill(KindOfSkill kindOfSkill)
        {
            GameObject bossSkillObject;
            switch (kindOfSkill)
            {
                case KindOfSkill.FireHemiSphere:
                    bossSkillObject = bossSkill[0];
                    break;
                case KindOfSkill.FireRain:
                    bossSkillObject = bossSkill[1];
                    break;
                case KindOfSkill.FireEntangle:
                    bossSkillObject = bossSkill[2];
                    break;
                default:
                    bossSkillObject = null;
                    break;
            }
            bossSkillObject.SetActive(true);
            return bossSkillObject;
        }




    }

}