using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;


public class AttackArea : MonoBehaviour {

    [SerializeField]
    Monster monster;
    Collider collider;


	// Use this for initialization
	void Start () {
        monster = transform.root.GetComponent<Monster>();
        collider = GetComponent<Collider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //데미지를 입히다//
            //other.Damage( monster.MonsterPower);
            //monster.player = other.GetComponent<Player>();
            monster.SendDamage(other.gameObject,monster.MonsterPower);

        }
    }

    public void SendDamage(GameObject target, int damage)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


}
