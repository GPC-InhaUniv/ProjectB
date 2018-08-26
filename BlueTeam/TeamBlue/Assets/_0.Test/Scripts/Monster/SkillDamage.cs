using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters;

public enum KindOfSkill
{
    FireHemiSphere = 200,
    FireRain = 100,
    FireEntangle = 400,
}

public class SkillDamage : MonoBehaviour {

    [SerializeField]
    Collider collider;


   
    [SerializeField]
    KindOfSkill kindOfSkill;

    // Use this for initialization
    void Start () {
        collider = GetComponent<Collider>();
        collider.enabled = true;
        
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

}
