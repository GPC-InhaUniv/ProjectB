using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectB.Characters.Monsters
{
    public class Named : Monster
    {


        void Start()
        {
            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            waitBaseTime = 2.0f;
            waitTime = waitBaseTime;

            attackable = new ComboAttack(animator);
            skillUsable = new NamedSkill(animator);

            SetMonsterInfo();
            monsterType = MonsterType.Named;
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
                //Died();

                ReceiveDamage(50);
            }


        }

    }
}
