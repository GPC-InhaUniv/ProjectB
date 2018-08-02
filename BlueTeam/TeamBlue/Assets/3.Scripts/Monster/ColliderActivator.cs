using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour {
    [SerializeField]
    Collider[] activeColliders;
	// Use this for initialization
	void Start () {

        AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
        activeColliders = new Collider[attackAreas.Length];

        for (int activeCollider = 0; activeCollider < attackAreas.Length; activeCollider++)
        {
            activeColliders[activeCollider] = attackAreas[activeCollider].GetComponent<Collider>();
            activeColliders[activeCollider].enabled = false;
        }

    }

    void StartAttack()
    {

        activeColliders[0].enabled = true;

    }
    void AttackEnd()
    {
        activeColliders[0].enabled = false;

    }
}
