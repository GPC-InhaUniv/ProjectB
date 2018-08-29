using System.Collections;
using UnityEngine;
using ProjectB.Characters;

namespace ProjectB.Characters.Monsters
{
    public enum KindOfSkill
    {
        FireHemiSphere = 200,
        FireRain = 100,
        FireEntangle = 400,
    }

    public class SkillDamage : MonoBehaviour
    {
        Collider collider;
        [SerializeField]
        KindOfSkill kindOfSkill;

        void Start()
        {
            collider = GetComponent<Collider>();
            collider.enabled = true;
            StartCoroutine(StopSkill());
        }
        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                Character player = other.GetComponent<Character>();
                player.ReceiveDamage((int)kindOfSkill);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Character player = other.GetComponent<Character>();
                player.ReceiveDamage((int)kindOfSkill);
            }
        }
        IEnumerator StopSkill()
        {
            yield return new WaitForSeconds(7.0f);
            gameObject.SetActive(false);
        }
    }
}