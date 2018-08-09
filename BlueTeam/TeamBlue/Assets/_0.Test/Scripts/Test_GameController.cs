using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectB.GameManager
{

    public class Test_GameController : MonoBehaviour
    {

        int currentStage;
        GameObject monsterSpawnPos;
        int monsterCount;


        public void SpawnPlayer()
        {
            Test_AssetBundleManager.Instance.AssetName = "Riko";
            GameObject player = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player);
        }
        
        public void SetMonster()
        {
            Test_AssetBundleManager.Instance.AssetName = "Riko";
            Test_PoolManager.Instance.monsterPrefab = Test_AssetBundleManager.Instance.LoadObject(BundleType.Player);
            Test_PoolManager.Instance.SetPool();
        }
    } 
}
