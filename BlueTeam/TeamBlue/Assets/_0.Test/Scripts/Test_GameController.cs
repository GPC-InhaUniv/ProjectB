using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectB.GameManager
{

    public class Test_GameController : MonoBehaviour
    {
        //LoadType
        int currentStage;
      
        int monsterCount;
        GameObject playerSpawnPos;
        List<GameObject> monsterSpawnPos;
        GameObject area;

        public void SpawnPlayer()
        {
            Test_AssetBundleManager.Instance.AssetName = "Riko";
            GameObject player = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player);
            Instantiate(player);
            player.transform.position = playerSpawnPos.transform.position;
        }
        
        public void SetPool()
        {
                Test_AssetBundleManager.Instance.AssetName = "Riko";
                Test_PoolManager.Instance.MonsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player);
            
                Test_AssetBundleManager.Instance.AssetName = "Village";
                Test_PoolManager.Instance.ParticlePrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Common);
            
                Test_PoolManager.Instance.SetPool();
        }

        public void NextScene()
        {

        }

        public void AddItem()
        {

        }

        public void StageClear()
        {

        }



    } 
}
