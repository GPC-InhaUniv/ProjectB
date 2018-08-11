using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Character;

public class AttackArea : MonoBehaviour {

    [SerializeField]
    Character characters;


    // Use this for initialization
    void Start()
    {
        characters = transform.root.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character player =  other.GetComponent<Character>();
            player.ReceiveDamage(characters.CharacterAttackPower);

        }
        else if(other.CompareTag("Monster"))
        {
            Character monster = other.GetComponent<Character>();
            monster.ReceiveDamage(characters.CharacterAttackPower);

           if (monster.CharacterHealthPoint <= 0)
            characters.SaveValue(monster.SendValue(Character.StatusType.CharacterExp));
        }
    }

    

}
