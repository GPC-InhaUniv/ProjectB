using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.UI.Minimap
{
    public class Radar : MonoBehaviour
    {
        public List<GameObject> Enemys = new List<GameObject>();

        void OnTriggerEnter(Collider other)
        {
            if (!(Enemys.Contains(other.gameObject)))
            {
                Enemys.Add(other.gameObject);
                Debug.Log("Enter");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (Enemys.Contains(other.gameObject))
            {
                Enemys.Remove(other.gameObject);
                Debug.Log("Exit");
            }
        }
        
    }
}
