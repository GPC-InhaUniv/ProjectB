using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.UI.Minimap
{
    public class Radar : MonoBehaviour
    {
        public class MinimapRadar : MonoBehaviour
        {

            public List<GameObject> Enemys { get { return enemys; } private set { } }
            [SerializeField]
            List<GameObject> enemys;

            void OnTriggerEnter(Collider other)
            {
                if (other.tag == "Monster")
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
        }
    }
}