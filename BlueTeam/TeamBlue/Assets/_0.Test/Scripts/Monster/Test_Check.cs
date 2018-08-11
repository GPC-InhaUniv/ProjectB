using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Character;

public class Test_Check : MonoBehaviour {

    

    enum KindOfSkill
    {
        IceRain,

    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnParticleCollision(GameObject other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("gogogo");
            Character player = other.GetComponent<Character>();
            player.ReceiveDamage(MonsterDefines.BossIceRainSkill);
        }


    }
}
