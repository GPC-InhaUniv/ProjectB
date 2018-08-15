using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.Minimaps
{
    public class MinimapRadar : MonoBehaviour
    {
        const float minimapScale = 5.0f;

        [SerializeField]
        GameObject player;

        [SerializeField]
        List<GameObject> enemys;

        [SerializeField]
        List<RectTransform> enemyIconPositions;

        Vector2 playerPosition;
        Vector2 enemyPosition;

        private void Start()
        {
          
        }
        void Update()
        {
            drawIcons();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!enemys.Contains(other.gameObject))
            {
                enemys.Add(other.gameObject);
            }
        }
      
        void OnTriggerExit(Collider other)
        {
            //Radar collider에서 사라지면 적 리스트에 들어온 적을 제거한다.
            if (enemys.Contains(other.gameObject))
            {
                enemys.Remove(other.gameObject);
            }
        }
       

        void drawIcons()
        {
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);

            for (int i = 0; i < enemys.Count; i++)
            {
                enemyPosition = new Vector2(enemys[i].transform.position.x, enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                enemyIconPositions[i].localPosition = playerToEnemy * minimapScale;
            }

            if (enemys.Count < enemyIconPositions.Count)
            {
                for (int i = enemys.Count; i < enemyIconPositions.Count; i++)
                {
                    enemyIconPositions[i].localPosition = new Vector3(100f, 0, 0);
                }
            }
        }
    }
}
