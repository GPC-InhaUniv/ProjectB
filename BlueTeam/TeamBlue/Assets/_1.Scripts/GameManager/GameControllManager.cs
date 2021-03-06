﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.UI.Presenter;
namespace ProjectB.GameManager
{
    public class GameControllManager : Singleton<GameControllManager>
    {
        const int MAXMONSTERCOUNT = 18;
        public LoadType CurrentLoadType;
        public int CurrentIndex;


       
        public bool IsClearDunegon { get; private set; }

       
        public int TotalExp { get; private set; }

        public int TotalMonsterCount { get; private set; }

        int cameraOffSetZ = 5;
        int cameraOffSetY = 2;
        int cameraOffSetX = 3;

        GameObject playerPosition;
        GameObject[] MonsterPostion;
        GameObject uiController;

        public Dictionary<int, int> ObtainedItemDic { get; private set; }
      

        private void Start()
        {
            ObtainedItemDic = new Dictionary<int, int>();
            TotalMonsterCount = MAXMONSTERCOUNT;
            MonsterPostion = new GameObject[3];
        }

        public void CheckMonsterAtDungeon()
        {
            TotalMonsterCount = MAXMONSTERCOUNT;

            TotalExp = 1200 * CurrentIndex;

            int itemCode = 0;

            switch (CurrentLoadType)
            {
             

                case LoadType.WoodDungeon: itemCode = 3000; break;
                case LoadType.IronDungeon: itemCode = 3001; break;
                case LoadType.BrickDungeon: itemCode = 3002; break;
                case LoadType.SheepDungeon: itemCode = 3003; break;
                default: Debug.Log("확인되어지지 않은 값"); break;

            }

            if(itemCode!=0)
            ObtainedItemDic.Add(itemCode, 1);

        }

        public void CheckGameOver()
        {
            IsClearDunegon = false;
            GameDataManager.Instance.AddPlayerExp();
            GameMediator.Instance.ClearStage();
            TotalMonsterCount = MAXMONSTERCOUNT;
        }

        void CalculateLevelUp()
        {
            GameDataManager.Instance.CalculatePlayerLevelUp();

        }
        public void CheckGameClear()
        {
            TotalMonsterCount--;

            CheckMonsterCount();


        }

       void CheckMonsterCount()
        {
            if (TotalMonsterCount == 0)
            {
                IsClearDunegon = true;
                GameDataManager.Instance.CalculatePlayerItemAndExp(TotalExp, ObtainedItemDic);
                GameMediator.Instance.ClearStage();
                TotalMonsterCount = MAXMONSTERCOUNT;
            }
        }




        public void MoveNextScene(LoadType loadType, int index)
        {
            CurrentLoadType = loadType;
            CurrentIndex = index;
            LoadingSceneManager.LoadScene(CurrentLoadType, CurrentIndex);

        }


        public void SetCameraPosition()
        {
            Transform tempCameraTransform;
            Transform playerTransform;

            tempCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
                tempCameraTransform.position = new Vector3(2.33f, 11f, 0.96f);
                tempCameraTransform.rotation = Quaternion.Euler(19.98f, 42.253f, 0.7f);

            }

            else
            {
                GameObject tempCamera = GameObject.FindGameObjectWithTag("MainCamera");
                tempCamera.AddComponent<FollowCamera>();
                tempCamera.GetComponent<Camera>().fieldOfView = 80;
            }
        }

        public void SetObjectPosition()
        {

            GameObject tempObject = GameObjectsManager.Instance.GetAreaObject();
            if (tempObject != null)
            {
                tempObject.SetActive(true);
                playerPosition = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");

            }

            tempObject = GameObjectsManager.Instance.GetPlayerObject();

            if (tempObject != null)
            {
                tempObject.SetActive(true);
                tempObject.transform.position = playerPosition.transform.position;

            }

            if (CurrentIndex != 0)
            {
                CheckMonsterAtDungeon();

                Debug.Log("현재 던전 몬스터 수:" + TotalMonsterCount);

            }

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {


            }
            else
            {

                MonsterPostion[0] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition1");
                MonsterPostion[1] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition2");
                MonsterPostion[2] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition3");

                int positionNum = 0;
                if (CurrentLoadType != LoadType.BossDungeon)
                {
                    for (int i = 0; i < TotalMonsterCount; i++)
                    {
                        Vector3 addPos = new Vector3(5, 0, 5);
                        if (i % 6 == 0)
                        {
                            GameObjectsManager.Instance.GetMonsterObject(Characters.Monsters.MonsterType.Named).transform.position = MonsterPostion[positionNum].transform.position;// + addPos;
                            if (i != 0)
                                positionNum++;
                        }
                        else
                        {
                            GameObjectsManager.Instance.GetMonsterObject(Characters.Monsters.MonsterType.Normal).transform.position = MonsterPostion[positionNum].transform.position;// + addPos;
                        }

                    }
                }
                else
                {
                    GameObjectsManager.Instance.GetMonsterObject(Characters.Monsters.MonsterType.Boss).transform.position = MonsterPostion[0].transform.position;
                }

                //     Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[0].transform.position;
                //    Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[1].transform.position;

            }


        }

        public void SetMonsterDropItem(int itemCode)
        {
            if (itemCode <= 0)
                return;

            if (ObtainedItemDic.ContainsKey(itemCode))
                ObtainedItemDic[itemCode]++;
            else
                ObtainedItemDic.Add(itemCode, 1);
        }

        public void SetBGM()
        {
            if(CurrentLoadType==LoadType.VillageCheckDownLoad)
            {
                SoundManager.Instance.LoadSoundClips();
                SoundManager.Instance.PlayBGM();

            }
        }


    }
}
