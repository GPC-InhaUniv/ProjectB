using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.Characters.Players
{
    public class MinimapRadar : MonoBehaviour
    {
        //Transform playerTransform;
            
        public List<GameObject> Enemys { get { return enemys; } private set { } }
        [SerializeField]
        List<GameObject> enemys;

        //private void Start()
        //{
        //    playerTransform = GetComponentInParent<Player>().transform;
        //}

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Monster")
            {
                if (!enemys.Contains(other.gameObject))
                {                    
                    enemys.Add(other.gameObject);
                }
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

        //플레이어 프레젠터에 있던 minimap 코드

        //MinimapRadar minimap;

        //[SerializeField]
        //public List<RectTransform> enemyIconPositions;

        //const float minimapScale = 5.0f;

        //Vector2 playerPosition;
        //Vector2 enemyPosition;

        //public RectTransform IconsParent;

        //[SerializeField]
        //RectTransform enemyIcon;
        //minimap

        //minimap
        //void RegistIcons()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        enemyIconPositions[i] = Instantiate(enemyIcon, IconsParent.rect.position, Quaternion.identity);
        //        enemyIconPositions[i].transform.parent = enemyIcon.parent;
        //    }
        //}

        //void DrawIcons()
        //{
        //    playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);

        //    for (int i = 0; i < minimap.Enemys.Count; i++)
        //    {
        //        enemyPosition = new Vector2(minimap.Enemys[i].transform.position.x, minimap.Enemys[i].transform.position.z);
        //        Vector2 playerToEnemy = enemyPosition - playerPosition;
        //        enemyIconPositions[i].localPosition = playerToEnemy * minimapScale;
        //    }

        //    if (minimap.Enemys.Count < enemyIconPositions.Count)
        //    {
        //        for (int i = minimap.Enemys.Count; i < enemyIconPositions.Count; i++)
        //        {
        //            enemyIconPositions[i].localPosition = new Vector3(100f, 0, 0);
        //        }
        //    }
        //}
        //minimap
    }
}
