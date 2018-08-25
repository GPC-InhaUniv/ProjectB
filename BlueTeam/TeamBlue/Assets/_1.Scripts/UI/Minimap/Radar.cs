using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters.Monsters;

namespace ProjectB.UI.Minimap
{
    public class Radar : MonoBehaviour
    {
        public List<GameObject> Enemys = new List<GameObject>();
        
        private void OnEnable()
        {
            Monster.NoticeToRader += RemoveFromList;
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (!(Enemys.Contains(other.gameObject)) && other.CompareTag("Monster"))
            {
                Enemys.Add(other.gameObject);
            }
        }

        void OnTriggerExit(Collider other) 
        {
            if (Enemys.Contains(other.gameObject))
            {
                Enemys.Remove(other.gameObject);
            }
        }

        void RemoveFromList(GameObject monster)
        {
            if (Enemys.Contains(monster) )
            {
                Enemys.Remove(monster);
            }
        }
    }
}
