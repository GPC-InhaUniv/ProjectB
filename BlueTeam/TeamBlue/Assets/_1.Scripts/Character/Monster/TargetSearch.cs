using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class TargetSearch : MonoBehaviour
    {
        Monster monster;
        void Start()
        {
            monster = GetComponent<Monster>();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                monster.SetAttackTarget(other.transform);
        }
    }
}