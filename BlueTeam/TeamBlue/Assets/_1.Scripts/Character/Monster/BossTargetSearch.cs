using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class BossTargetSearch : MonoBehaviour
    {
        Monster monster;
        void Start()
        {
            monster = GetComponent<Monster>();
        }
        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                float fieldOfView = 120.0f;
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle < fieldOfView * 0.5)
                    monster.SetAttackTarget(other.transform);
            }
        }
    }
}