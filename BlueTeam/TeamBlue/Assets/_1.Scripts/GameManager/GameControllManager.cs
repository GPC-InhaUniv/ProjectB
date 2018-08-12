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

        int totalMonsterCount;
        int cameraOffSetZ = 4;
        int cameraOffSetY = 3;
        int cameraOffSetX = 3;
        GameObject playerPosition;
        GameObject[] MonsterPostion;

        private void Start()
        {
            MonsterPostion = new GameObject[3];
        }
        public void CheckMonsterAtDungeon()
        {
            totalMonsterCount = CurrentIndex * 10;
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


            Transform tempCameraTranform;

            tempCameraTranform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
                tempCameraTranform.position = new Vector3(2.33f, 2.19f, 0.96f);
                tempCameraTranform.rotation = Quaternion.Euler(19.98f, 42.253f, 0.7f);

            }
            else
            {
                
                MonsterPostion[0] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition1");
                MonsterPostion[1] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition2");
                MonsterPostion[2] = GameObject.FindGameObjectWithTag("MonsterSpawnPosition3");

                Test_PoolManager.Instance.GetMonsterObject().transform.position = MonsterPostion[0].transform.position;

                tempCameraTranform.LookAt(playerPosition.transform);
                tempCameraTranform.position = new Vector3(playerPosition.transform.position.x-cameraOffSetX, playerPosition.transform.position.y, playerPosition.transform.position.z - cameraOffSetZ);
            }


        }





    }
}
