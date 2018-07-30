using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;


public abstract class AttackArea : MonoBehaviour {

    [SerializeField]
    Monster monster;



	// Use this for initialization
	void Start () {
        monster = transform.root.GetComponent<Monster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected void AttackEnd()
    {

    }
}
