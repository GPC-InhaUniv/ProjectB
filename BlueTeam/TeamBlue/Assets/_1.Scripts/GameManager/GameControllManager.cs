using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.GameManager
{
    public class GameControllManager : Singleton<GameControllManager> {

        public LoadType CurrentLoadType;
        public int CurrentIndex;

        bool isClearDungeon;
        bool isGameOver;

        int totalMonsterCount;

        GameObject playerPosition;
        GameObject[] MonsterPostion;
        
        public void CheckMonsterAtDungeon()
        {
            totalMonsterCount = CurrentIndex * 10;
        }

        // Use this for initialization
        void Start() {
              
        }

        // Update is called once per frame
        void Update() {
           
        }

        public void MoveNextScene(LoadType loadType,int index)
        {
            CurrentLoadType = loadType;
            CurrentIndex = index;
            Debug.Log(CurrentIndex);
            LoadingSceneManager.LoadScene(CurrentLoadType, CurrentIndex);

        }

        public void SetObjectPosition()
        {
            GameObject tempObject = Test_PoolManager.Instance.GetArea();

            if (tempObject != null)
            {
                tempObject.SetActive(true);
                playerPosition=GameObject.FindGameObjectWithTag("PlayerSpawnPosition");

          

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
                Debug.Log(totalMonsterCount);

            }

            if (CurrentLoadType == LoadType.Village || CurrentLoadType == LoadType.VillageCheckDownLoad)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(2.33f, 2.19f, 0.96f);
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().rotation = Quaternion.Euler(19.98f, 42.253f, 0.7f);

            }
         

        }


    


    }
}
