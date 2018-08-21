using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.UI.Minimap
{
    public class MinimapRadar : MonoBehaviour
    {
        public GameObject Player;
        public List<GameObject> Enemys;
        // Use this for initialization

        //포지션 저장할 위치
        Vector2 playerPosition;
        Vector2 enemyPosition;

        public List<RectTransform> EnemyIconsPosition;
        
        void Update()
        {
            DrawIcons();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Enemys.Contains(other.gameObject))
            {
                Enemys.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (Enemys.Contains(other.gameObject))
            {
                Enemys.Remove(other.gameObject);
            }
        }

        
        void DrawIcons()
        {
            playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.z);

            for (int i = 0; i < Enemys.Count; i++)
            {
                enemyPosition = new Vector2(Enemys[i].transform.position.x, Enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                EnemyIconsPosition[i].localPosition = playerToEnemy * 4.5f;
            }

            if (Enemys.Count < EnemyIconsPosition.Count)
            {
                for (int i = Enemys.Count; i < EnemyIconsPosition.Count; i++)
                {
                    EnemyIconsPosition[i].localPosition = new Vector3(100f, 0, 0);
                }
            }
        }
    }
}
