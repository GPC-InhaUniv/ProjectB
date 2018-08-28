using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.UI.Presenter;
namespace ProjectB.GameManager
{
    public class GameControllManager : Singleton<GameControllManager>
    {

        public LoadType CurrentLoadType;
        public int CurrentIndex;


        bool isClearDungeon;
        public bool IsClearDungeon { get { return isClearDungeon; } private set { } }

        int totalExp;
        public int TotalExp { get { return totalExp; } private set { } }

        int totalMonsterCount;
        int cameraOffSetZ = 5;
        int cameraOffSetY = 2;
        int cameraOffSetX = 3;

        GameObject playerPosition;
        GameObject[] MonsterPostion;
        GameObject uiController;

        public Dictionary<int, int> ObtainedItemDic = new Dictionary<int, int>();

        private void Start()
        {
            MonsterPostion = new GameObject[3];

        }
        public void CheckMonsterAtDungeon()
        {
            totalMonsterCount = GameObjectsManager.Instance.MonsterPoolSize;
            totalExp = 1200 * CurrentIndex;


            switch (CurrentLoadType)
            {
                case LoadType.WoodDungeon:
                    ObtainedItemDic.Add(3000, CurrentIndex * 5);
                    break;
                case LoadType.IronDungeon:
                    ObtainedItemDic.Add(3001, CurrentIndex * 5);
                    break;
                case LoadType.BrickDungeon:
                    ObtainedItemDic.Add(3002, CurrentIndex * 5);
                    break;
                case LoadType.SheepDungeon:
                    ObtainedItemDic.Add(3003, CurrentIndex * 5);
                    break;

            }

        }

        public void CheckGameOver()
        {
            isClearDungeon = false;
            GameMediator.Instance.ClearStage();
        }

        void CalculateLevelUp()
        {
            float currentLevel = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
            float currentExp = GameDataManager.Instance.PlayerInfomation.PlayerExp;

            float nextLevepUpExp = 1000 + (100 * 1.2f * currentLevel);
            if (nextLevepUpExp <= currentExp)
            {
                currentLevel++;
                currentExp -= nextLevepUpExp;
            }

            GameDataManager.Instance.PlayerInfomation.PlayerLevel = (int)currentLevel;
            GameDataManager.Instance.PlayerInfomation.PlayerExp = currentExp;

        }
        public void CheckGameClear()
        {
            totalMonsterCount--;
            if (totalMonsterCount <= 0)
            {
                isClearDungeon = true;
                GameDataManager.Instance.PlayerInfomation.PlayerExp += totalExp;
                foreach (KeyValuePair<int, int> temp in ObtainedItemDic)
                {
                    GameDataManager.Instance.PlayerGamedata[temp.Key] += temp.Value;
                }
                ObtainedItemDic.Clear();
                CalculateLevelUp();
                GameDataManager.Instance.SetGameDataToServer();
                GameMediator.Instance.ClearStage();
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

                Debug.Log("현재 던전 몬스터 수:" + totalMonsterCount);

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
                for (int i = 0; i < GameObjectsManager.Instance.MonsterPoolSize; i++)
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

                //     Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[0].transform.position;
                //    Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[1].transform.position;

            }


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
