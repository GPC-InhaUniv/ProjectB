using MonsterAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Named : Monster
{


    void Start()
    {
        monsterMove = GetComponent<MonsterMove>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        waitBaseTime = 2.0f;
        waitTime = waitBaseTime;

        //test//
        AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();


        attackable = new NormalAttack();
        skillUsable = new NamedSkill();

        walkRange = 5;
        speed = 3;

        

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
            Died();


    }

    protected override IEnumerator WaitNextState()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("Attack", 0);


        ChangeState(State.Chasing);
        Debug.Log("gogogo");
    }
}
