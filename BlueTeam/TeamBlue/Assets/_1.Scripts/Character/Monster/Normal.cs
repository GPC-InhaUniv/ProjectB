using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class Normal : Monster
    {

        void Start()
        {
            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            waitBaseTime = 2.0f;
            waitTime = waitBaseTime;

            attackable = new NormalAttack(animator);
            skillUsable = new NoSkill(animator);

            SetMonsterInfo();
            monsterType = MonsterType.Normal;
            walkRange = 15;
            skillCoolTime = 10;
            speed = 2;

            hitParticle.SetActive(false);
        }
        void Update()
        {

            switch (state)
            {
                case State.Walking:
                    WalkAround();                
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
                case State.Died:
                    Died();
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
                        NoSkill.SetState += ChangeStateToWalking;
                        UseSkill();
                        NoSkill.SetState -= ChangeStateToWalking;
                        break;
                    case State.Died:
                        Died();
                        break;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Died();

                ReceiveDamage(50);
            }
        }

    }
}