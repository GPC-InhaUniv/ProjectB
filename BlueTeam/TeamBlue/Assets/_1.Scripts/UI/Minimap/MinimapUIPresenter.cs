using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

namespace ProjectB.UI.Minimap
{
    public class MinimapUIPresenter : MonoBehaviour //,IInitializable
    {
        
        Radar radar;
        Transform radarPosition;
       
        public List<RectTransform> EnemyIconsPosition;//이놈을 어떻게 받아올까 ...
        //public GameObject[] EnemyIconsPosition;
        //RectTransform[] test;
        Vector2 playerPosition;
        Vector2 enemyPosition;

        const float mapScale = 5.0f;
        const float defaultIconPositionX = 100.0f;
        const float minimapUpdateTime = 0.1f;
        const int sizeOfIconArray = 20;

        
        void Start()
        {
            //EnemyIconsPosition = new GameObject[sizeOfIconArray];
            //EnemyIconsPosition = GameObject.FindGameObjectsWithTag("Minimap");
            StartCoroutine(StartDrawIcon());
            Debug.Log(EnemyIconsPosition);
            radarPosition.transform.position = radar.transform.position;
        }
        
        IEnumerator StartDrawIcon()
        {
            DrawIcons();
            yield return new WaitForSeconds(minimapUpdateTime);
            StartCoroutine(StartDrawIcon());
        }

        void DrawIcons()
        {
            playerPosition = new Vector2(radar.transform.position.x, radar.transform.position.z);

            for (int i = 0; i < radar.Enemys.Count; i++)
            {
                enemyPosition = new Vector2(radar.Enemys[i].transform.position.x, radar.Enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                EnemyIconsPosition[i].transform.localPosition = playerToEnemy * mapScale;
            }

            if (radar.Enemys.Count < EnemyIconsPosition.Count)
            {
                for (int i = radar.Enemys.Count; i < EnemyIconsPosition.Count; i++) 
                {
                    EnemyIconsPosition[i].localPosition= new Vector3(defaultIconPositionX, 0, 0);
                }
            }
        }

        //public void Initialize()
        //{
            
        //}
    }
}
