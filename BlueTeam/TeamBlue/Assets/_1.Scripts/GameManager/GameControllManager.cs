using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.GameManager
{
    public class GameControllManager : Singleton<GameControllManager> {

        LoadType currentLoadType;
        int currentIndex;

        bool isClearDungeon;
        bool isGameOver;

        int totalMonsterCount;

        GameObject playerPosition;
        GameObject[] MonsterPostion;

        public void CheckMonsterAtDungeon()
        {
            totalMonsterCount = currentIndex * 10;
        }

        // Use this for initialization
        void Start() {
              
        }

        // Update is called once per frame
        void Update() {

        }

        public void MoveNextScene(LoadType loadType,int index)
        {
            currentLoadType = loadType;
            currentIndex = index;
            LoadingSceneManager.LoadScene(currentLoadType, currentIndex);


            if (currentIndex != 0)
            {
                CheckMonsterAtDungeon();

            }

        }

        public void SetObjectPosition()
        {
            GameObject tempObject = Test_PoolManager.Instance.GetArea();
            if (tempObject != null)
            {
                tempObject.SetActive(true);
                playerPosition=GameObject.FindGameObjectWithTag("PlayerSpawnPostiion");
                

            }
            tempObject = Test_PoolManager.Instance.GetPlayer();
            if (tempObject != null)
            {
                tempObject.SetActive(true);
                tempObject.transform.position = playerPosition.transform.position;
            }
        }

    


    }
}
