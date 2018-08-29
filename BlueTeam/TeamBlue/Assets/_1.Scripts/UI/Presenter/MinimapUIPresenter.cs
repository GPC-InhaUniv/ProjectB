using System.Collections;
using UnityEngine;
using ProjectB.GameManager;

namespace ProjectB.UI.Minimap
{
    public class MinimapUIPresenter : MonoBehaviour ,IInitializable
    {
        Radar radar;
        GameObject[] enemyIconsPosition;
        Vector2 playerPosition;
        Vector2 enemyPosition;

        const float mapScale = 9.0f;
        const float defaultIconPositionX = 100.0f;
        const float minimapUpdateTime = 0.1f;
        const int sizeOfIconArray = 24;
        
        public void Initialize()
        {
            radar = GameObject.FindGameObjectWithTag("Radar").GetComponent<Radar>();
            enemyIconsPosition = new GameObject[sizeOfIconArray];
            enemyIconsPosition = GameObject.FindGameObjectsWithTag("Minimap");
            StartCoroutine(StartDrawIcon());
        }

        IEnumerator StartDrawIcon()
        {
            while(true)
            {
                DrawIcons();
                yield return new WaitForSeconds(minimapUpdateTime);
            }
        }

        public void ResetRadar()
        {
            StopCoroutine(StartDrawIcon());
            ResetIconPosition();
            radar.ClearEnemyList();
        }

        void DrawIcons()
        {
            playerPosition = new Vector2(radar.gameObject.transform.position.x, radar.gameObject.transform.position.z);

            for (int i = 0; i < radar.Enemys.Count; i++)
            {
                enemyPosition = new Vector2(radar.Enemys[i].transform.position.x, radar.Enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                enemyIconsPosition[i].transform.localPosition = playerToEnemy * mapScale;
            }

            if (radar.Enemys.Count < enemyIconsPosition.Length)
            {
                ResetIconPosition();
            }
        }

        void ResetIconPosition()
        {
            for (int i = radar.Enemys.Count; i < enemyIconsPosition.Length; i++)
            {
                enemyIconsPosition[i].transform.localPosition = new Vector3(defaultIconPositionX, 0, 0);
            }
        }
    }
}
