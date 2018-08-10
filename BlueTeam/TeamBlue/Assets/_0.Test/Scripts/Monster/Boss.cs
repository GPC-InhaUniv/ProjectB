﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Character.Monster
{
    public class Boss : Monster
    {
        BossState bossState;

        void Start()
        {
            bossState = new PhaseOne(this, skillprefab);

            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            waitBaseTime = 2.0f;
            waitTime = waitBaseTime;

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
        protected override void AttackTarget()
        {
            bossState.Attack();
        }
        protected override void UseSkill()
        {
            bossState.UseSkill();
        }
        public override void ReceiveDamage(int damage)
        {
            animator.SetTrigger("Hitted");
            CharacterHealthPoint -= damage;

            if (CharacterHealthPoint <= CharacterMaxHealthPoint * 0.5)
            {
                bossState = new PhaseTwo(this, skillprefab);
            }
            else if (CharacterHealthPoint <= 0)
            {
                CharacterHealthPoint = 0;
                ChangeState(State.Died);
            }
        }



        //public void HandleState(string state)
        //{
        //    switch (state)
        //    {
        //        case "AnnoyedState":
        //            BossState = new AnnoyedState(rigidbody);
        //            break;

        //        case "AngerState":
        //            BossState = new AngerState(rigidbody);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}
