using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;


public class Boss : Monster
{

    void Start()
    {
        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        waitBaseTime = 2.0f;
        waitTime = waitBaseTime;

        attackable = new ComboAttack();
        skillUsable = new NamedSkill(this, skillprefab);

    }
    void Update()
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

        if (state != currentState)
        {
            state = currentState;
            switch (state)
            {
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Died();

        }
    }
}
