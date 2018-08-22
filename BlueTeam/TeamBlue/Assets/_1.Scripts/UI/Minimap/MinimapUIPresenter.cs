using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Characters.Players;

namespace ProjectB.UI.Minimap
{
    public class MinimapUIPresenter : MonoBehaviour ,IInitializable
    {
        Radar radar;
        //public List<RectTransform> EnemyIconsPosition;//이놈을 어떻게 받아올까 ...
        GameObject[] EnemyIconsPosition;
        Vector2 playerPosition;
        Vector2 enemyPosition;

        const float mapScale = 5.0f;
        const float defaultIconPositionX = 100.0f;
        const float minimapUpdateTime = 0.1f;
        const int sizeOfIconArray = 24;
        
        void Start()
        {
            radar = GameObject.FindGameObjectWithTag("Player").GetComponent<Radar>();
            EnemyIconsPosition = new GameObject[sizeOfIconArray];
            EnemyIconsPosition = GameObject.FindGameObjectsWithTag("Minimap");
            StartCoroutine(StartDrawIcon());
        }
        public void Initialize()
        {
            //radar = GameObject.FindGameObjectWithTag("Player").GetComponent<Radar>();
            //EnemyIconsPosition = new GameObject[sizeOfIconArray];
            //EnemyIconsPosition = GameObject.FindGameObjectsWithTag("Minimap");
            //StartCoroutine(StartDrawIcon());
        }

       

        IEnumerator StartDrawIcon()
        {
            DrawIcons();
            yield return new WaitForSeconds(minimapUpdateTime);
            StartCoroutine(StartDrawIcon());
        }

        void DrawIcons()
        {
            playerPosition = new Vector2(radar.gameObject.transform.position.x, radar.gameObject.transform.position.z);

            for (int i = 1; i < radar.Enemys.Count; i++)
            {
                enemyPosition = new Vector2(radar.Enemys[i].transform.position.x, radar.Enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                EnemyIconsPosition[i].transform.localPosition = playerToEnemy * mapScale;
                Debug.Log(i);
            }

            if (radar.Enemys.Count < EnemyIconsPosition.Length)
            {
                for (int i = radar.Enemys.Count; i < EnemyIconsPosition.Length; i++) 
                {
                    EnemyIconsPosition[i].transform.localPosition= new Vector3(defaultIconPositionX, 0, 0);
                }
            }
        }
    }
}
