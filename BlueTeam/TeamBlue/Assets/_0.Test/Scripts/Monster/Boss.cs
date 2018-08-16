using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class Boss : Monster
    {
        BossState bossState;
        public ISkillUsableBridge DefencSkillUsable;
        public ISkillUsableBridge EntangleSkillUsable;
        float stateHandleNum;


        void Start()
        {
            //bossState = new PhaseOne(this, skillprefab);
            bossState = new PhaseTwo(this, skillprefab);

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
            if (!isInvincibility)
            {
                int defencePossibility = Random.Range(1, 9);
                if (defencePossibility == 1)
                {
                    animator.SetTrigger(AniStateParm.Defence.ToString());
                }
                else if (defencePossibility == 2)
                {
                    animator.SetTrigger(AniStateParm.Defence.ToString());
                    bossState.UseDefenceSkill();
                }
                else
                {
                    animator.SetTrigger(AniStateParm.Hitted.ToString());
                    characterHealthPoint -= damage;

                    if (CharacterHealthPoint <= CharacterMaxHealthPoint * (2 / 3) && stateHandleNum == 0)
                    {
                        bossState = new PhaseTwo(this, skillprefab);
                        stateHandleNum++;
                    }
                    else if (CharacterHealthPoint <= CharacterMaxHealthPoint * (1 / 3) && stateHandleNum == 1)
                    {
                        bossState = new PhaseThree(this, skillprefab);
                        stateHandleNum++;
                    }
                    else if (CharacterHealthPoint <= 0)
                    {
                        characterHealthPoint = 0;
                        ChangeState(State.Died);
                    }
                }

            }
        }
    }
}
