using MonsterAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Named : Monster {

    
    // Use this for initialization
    void Start()
    {

        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;

        waitBaseTime = 2.0f;

        waitTime = waitBaseTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning)
        {
            switch (state)
            {
                case State.Walking:
                    Walkaround();
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
            }
        }
        if (state != currentState)
        {
            state = currentState;
            switch (state)
            {
                case State.Walking:
                    Walkaround();
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
                case State.Attacking:
                    AttackTarget();
                    break;
                case State.Skilling:
                    UseSkill();
                    break;
                case State.Died:
                    Died();
                    break;
            }
        }


    }
    protected override void AttackTarget()
    {

        animator.SetInteger("Attack",1);

        Debug.Log("ATTTTTAck");

    }

    protected override void UseSkill()
    {
        throw new System.NotImplementedException();
    }



}
