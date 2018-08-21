using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class Normal : Monster
    {
        private void OnEnable()
        {
            NoSkill.Setstate += ChangeStateToChasing;

        }
        private void OnDisable()
        {
            NoSkill.Setstate -= ChangeStateToChasing;

        }

        void Start()
        {
            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            waitBaseTime = 2.0f;
            waitTime = waitBaseTime;


            attackable = new NormalAttack(animator);
            skillUsable = new NoSkill(animator);



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
                Died();


        }

    }
}