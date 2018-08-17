using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.GameManager
{
    public class GameControllManager : Singleton<GameControllManager>
    {

        public LoadType CurrentLoadType;
        public int CurrentIndex;

        bool isClearDungeon;
        bool isGameOver;

        int totalExp;
        int totalMonsterCount;
        int cameraOffSetZ = 5;
        int cameraOffSetY = 2;
        int cameraOffSetX = 3;

        GameObject playerPosition;
        GameObject[] MonsterPostion;
        List<Item> obtainedItemList = new List<Item>();
        public List<Item> ObtainedItemList { get { return obtainedItemList; } private set { } }

        private void Start()
        {
            MonsterPostion = new GameObject[3];
        }
        public void CheckMonsterAtDungeon()
        {
            totalMonsterCount = CurrentIndex * 10;
            totalExp = 1200 * CurrentIndex;
        }

        public void CheckGameOver()
        {
            isGameOver = true;
            //경험치 까기
        }
        public void CheckGameClear()
        {
            totalMonsterCount--;
            if(totalMonsterCount<=0)
            {
                isClearDungeon = true;
                GameDataManager.Instance.PlayerInfomation.PlayerExp += totalExp;
            }
        }

        public void SetObjectPool()
        {

        }

        public void MoveNextScene(LoadType loadType, int index)
        {
            CurrentLoadType = loadType;
            CurrentIndex = index;
            LoadingSceneManager.LoadScene(CurrentLoadType, CurrentIndex);

        }

        public void SetUI()
        {
            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
                Debug.Log("마을 UI 로드");
            }
            else
            {
                Debug.Log("인게임 UI 로드");
                GameObject tempUIObject = Test_PoolManager.Instance.GetInGamePanel();
                tempUIObject.transform.SetParent(GameObject.Find("Canvas").transform);
                tempUIObject.transform.position = new Vector3(645, 374, 0);

                tempUIObject.gameObject.SetActive(true);
            }
        }

        public void SetCameraPosition()
        {
            Transform tempCameraTransform;
            Transform playerTransform;

            tempCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
                tempCameraTransform.position = new Vector3(2.33f, 2.19f, 0.96f);
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
            GameObject tempObject = Test_PoolManager.Instance.GetArea();

            if (tempObject != null)
            {
                tempObject.SetActive(true);
                playerPosition = GameObject.FindGameObjectWithTag("PlayerSpawnPosition");



            }

            tempObject = Test_PoolManager.Instance.GetPlayer();

            if (tempObject != null)
            {
                tempObject.SetActive(true);
                tempObject.transform.position = playerPosition.transform.position;

            }

            if (CurrentIndex != 0)
            {
                CheckMonsterAtDungeon();
                
                Debug.Log("현재 던전 몬스터 수:"+totalMonsterCount);

            }

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
              

            }
            else
            {
                
                MonsterPostion[0] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition1");
                MonsterPostion[1] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition2");
                MonsterPostion[2] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition3");

                Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[0].transform.position;
                Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[1].transform.position;

            }


        }





    }
}
