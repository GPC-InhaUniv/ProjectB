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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning)
        {
            switch (state)
            {
                case State.Walking:
                    StartCoroutine(Walk());
                    Debug.Log("ggo");
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


        //look toward player
        //Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        //Debug.Log(targetDirection);
        //monsterMove.SetDirection(targetDirection);
        //monsterMove.StopMove();
            animator.SetTrigger("Attack");


        //if (charaAnimation.IsAttacked())
        //    ChangeState(State.Walking);
        // 대기 시간을 다시 설정한다.
        // 타겟을 리셋한다.
    }

    protected override void UseSkill()
    {
        throw new System.NotImplementedException();
    }

    //protected void Endattack()
    //{
    //    ChangeState(State.Chasing);
    //    Debug.Log("EndAttack");
    //}

}
