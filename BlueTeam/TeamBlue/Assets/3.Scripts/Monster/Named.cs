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
        StartCoroutine(Waliking());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCoroutineRunning)
        {
            switch (state)
            {
                case State.Walking:
                    StartCoroutine(Waliking());
                    Debug.Log("ggo");
                    break;
                case State.Chasing:
                    Chasing();
                    break;
                case State.Attacking:
                    Attack();
                    break;
                case State.Skilling:
                    SkillUse();
                    break;
                case State.Died:
                    Died();
                    break;
            }
        }
    }
    protected override void Attack()
    {
        attacking = true;

        //look toward player
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        monsterMove.SetDirection(targetDirection);
        monsterMove.StopMove();
        if (attacking == true)
        {
            animator.SetInteger("moving", 3);
            attacking = false;
            //ChangeState(State.Walking);
            Debug.Log("물어");

            //타겟이 바로 해제되지않게 할것
            attackTarget = null;

        }


        //if (charaAnimation.IsAttacked())
        //    ChangeState(State.Walking);
        // 대기 시간을 다시 설정한다.
        // 타겟을 리셋한다.
    }

    protected override void SkillUse()
    {
        throw new System.NotImplementedException();
    }

}
