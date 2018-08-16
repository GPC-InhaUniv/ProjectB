using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters;

public class Test_Check : MonoBehaviour {

    [SerializeField]
    Collider collider;
    

    enum KindOfSkill
    {
        FireHemiSphere = 15,
        FireRain = 30,
        FireEntangle = 55,


    }
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
            Debug.Log("gogogo");
            Character player = other.GetComponent<Character>();
            player.ReceiveDamage((int)kindOfSkill);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("gogogo");
            Character player = other.GetComponent<Character>();
            player.ReceiveDamage((int)kindOfSkill);
        }
    }

}
