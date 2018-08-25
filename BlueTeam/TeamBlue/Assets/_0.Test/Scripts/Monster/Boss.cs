using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Characters;
using ProjectB.GameManager;

namespace ProjectB.Characters.Monsters
{
    public class Boss : Monster
    {
        BossState bossState;
        ISkillUsableBridge defencSkillUsable;
        ISkillUsableBridge entangleSkillUsable;
        float stateHandleNum;

        private void OnEnable()
        {
            NoSkill.SetState += ChangeStateToWalking;

        }
        private void OnDisable()
        {
            NoSkill.SetState -= ChangeStateToWalking;

        }
        void Start()
        {
            //Setstate

            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;
            waitBaseTime = 2.0f;
            waitTime = waitBaseTime;


            //bossState = new PhaseOne(animator, skillprefab, attackable, defencSkillUsable, skillUsable);
            //bossState = new PhaseTwo(animator, skillprefab, attackable, defencSkillUsable, skillUsable);
            bossState = new PhaseThree(animator, skillprefab,attackable,defencSkillUsable,skillUsable,entangleSkillUsable);

            characterHealthPoint = characterMaxHealthPoint;

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
            //Debug.Log(animator.GetInteger(AniStateParm.Attack.ToString()));

            if (Input.GetKeyDown(KeyCode.F))
            {
                Died();
            }
        }
        protected override void AttackTarget()
        {
            bossState.Attack();
            SoundManager.Instance.SetSound(SoundFXType.EnemyAttack);

        }
        protected override void UseSkill()
        {
            bossState.UseSkill();
        }
        public override void ReceiveDamage(float damage)
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

                    SoundManager.Instance.SetSound(SoundFXType.EnemyHit);


                    if (CharacterHealthPoint <= CharacterMaxHealthPoint * (2 / 3) && stateHandleNum == 0)
                    {
                        bossState = new PhaseTwo(animator, skillprefab, attackable, defencSkillUsable, skillUsable);

                        stateHandleNum++;
                    }
                    else if (CharacterHealthPoint <= CharacterMaxHealthPoint * (1 / 3) && stateHandleNum == 1)
                    {
                        bossState = new PhaseThree(animator, skillprefab,attackable,defencSkillUsable,skillUsable,entangleSkillUsable);
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
