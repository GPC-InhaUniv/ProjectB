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

        GameObject areaPrefab;
        GameObject playerPrefab;
        GameObject nomalMonsterPrefab;
        GameObject namedMonsterPrefab;
        GameObject bossMonsterPrefab;
        public GameObject[] bossSkill;
        GameObject gameCanvasPrefab;

        int monsterPoolSize;
        private void Start()
        {
            bossSkill = new GameObject[3];
            DontDestroyOnLoad(gameObject);
            
        }


        public void SetPrefab()
        {
            playerPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Player, "PlayerCharacter");
            gameCanvasPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Common, "MainCanvas");

        }

        public void SetAreaPrefab(int stageNum)
        {
            Debug.Log(GameControllManager.Instance.CurrentLoadType);
            int areaNum = Mathf.Abs(stageNum % 3 + 1);
            if (GameControllManager.Instance.CurrentLoadType != LoadType.VillageCheckDownLoad && GameControllManager.Instance.CurrentLoadType != LoadType.Village)
                areaPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Area, "Stage" + areaNum.ToString());
            else
            {

                areaPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Area, "Village");
            }
        }

        public void SetMonsterPrefab()
        {

            nomalMonsterPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Area, "Normal");
            namedMonsterPrefab = AssetBundleManager.Instance.LoadObject(BundleType.Area, "Named");
            if (GameControllManager.Instance.CurrentLoadType == LoadType.BossDungeon)
            {
               
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
            if (bossSkill != null)
                for (int i = 0; i < bossSkill.Length; i++)
                {
                    Destroy(bossSkill[i]);
                }
            if (bossMonster != null)
                Destroy(bossMonster);
            curruntNormalMonsterIndex = 0;
            curruntNamedMonsterIndex = 0;
        }

        //Pool

        GameObject[] normalMonster;
        GameObject[] namedMonster;
        GameObject bossMonster;

        int curruntNormalMonsterIndex = 0;
        int curruntNamedMonsterIndex = 0;

        public void SetPool()
        {
            monsterPoolSize = GameControllManager.Instance.TotalMonsterCount;
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
                namedMonster[i].SetActive(false);
            }
            if (GameControllManager.Instance.CurrentLoadType == LoadType.BossDungeon)
            {
                bossMonster = Instantiate(AssetBundleManager.Instance.LoadObject(BundleType.Area, "Boss"));
                DontDestroyOnLoad(bossMonster);
                bossMonster.SetActive(false);
                bossSkill[0] = Instantiate(AssetBundleManager.Instance.LoadObject(BundleType.Area, "SkillFireEntangle"));
                bossSkill[1] = Instantiate(AssetBundleManager.Instance.LoadObject(BundleType.Area, "SkillFireRain"));
                bossSkill[2] = Instantiate(AssetBundleManager.Instance.LoadObject(BundleType.Area, "SkillHemiSphere"));
               
                for(int i = 0; i <bossSkill.Length;i++)
                {
                    DontDestroyOnLoad(bossSkill[i]);
                    bossSkill[i].SetActive(false);
                }
               
            }

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
                    monsterObject.SetActive(true);
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