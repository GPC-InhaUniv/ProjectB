using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;


public class AttackArea : MonoBehaviour {

    [SerializeField]
    Monster monster;



	// Use this for initialization
	void Start () {
        monster = transform.root.GetComponent<Monster>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //데미지를 입히다//
            //other.Damage( monster.MonsterPower);

        }
    }

}
