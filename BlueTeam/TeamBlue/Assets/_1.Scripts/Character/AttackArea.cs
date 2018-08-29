using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters;

public class AttackArea : MonoBehaviour {

    [SerializeField]
    Character characters;

    void Start()
    {
        characters = transform.root.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character player =  other.GetComponent<Character>();
            player.ReceiveDamage(characters.AttackPower);
        }
        else if(other.CompareTag("Monster"))
        {
            Character monster = other.GetComponent<Character>();
            monster.ReceiveDamage(characters.AttackPower);
            characters.transform.LookAt(other.transform);
        }
    }
}
