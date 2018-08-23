using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.UI.Minimap
{
    public class Radar : MonoBehaviour
    {
        public List<GameObject> Enemys = new List<GameObject>();

        private void Start()
        {
           
        }



        void OnTriggerEnter(Collider other)
        {
            if (!(Enemys.Contains(other.gameObject)))
            {
                Enemys.Add(other.gameObject); // Enemys목록에 해당 오브젝트가 없으면 추가시킨다.....
            }
        }

        void OnTriggerExit(Collider other) // OnTriggerExit는 Destroy나 Disable됐을 경우 호출되지 않는다.
        {
            if (Enemys.Contains(other.gameObject))
            {
                Enemys.Remove(other.gameObject);
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (Enemys.Contains(other.gameObject) && other.gameObject.activeInHierarchy == false)
            {
                    Enemys.Remove(other.gameObject);
            }
        }
    }
}
