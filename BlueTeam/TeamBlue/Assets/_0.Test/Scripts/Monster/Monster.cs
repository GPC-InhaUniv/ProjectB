﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;


namespace ProjectB.Characters.Monsters
{
    public enum MonsterType
    {
        Normal,
        Named,
        Boss,
    }


    public enum AniStateParm
    {
        Moving,
        Battle,
        Hitted,
        Attack,
        SkillOne,
        SkillTwo,
        Defence,
        Died,
    }

    public abstract class Monster : Character
    {

        [SerializeField]
        protected MonsterType monsterType;

        [SerializeField]
        protected GameObject[] skillprefab;

        //Monster Status//
        [SerializeField]
        protected int walkRange;

        [SerializeField]
        protected float skillCoolTime;
        [SerializeField]
        protected bool attacking, died, skillUse;
        [SerializeField]
        protected GameObject[] dropItemPrefab;
        //Monster System//
        [SerializeField]
        protected float waitBaseTime;
        [SerializeField]
        protected float waitTime, speed;
        //Set Target//
       // [SerializeField]
        public Transform attackTarget;
        [SerializeField]
        protected MonsterMove monsterMove;
        //Move To Destination//
        [SerializeField]
        protected Vector3 startPosition;

        int maxPercent;


        // Monster State//
        public enum State
        {
            Walking,    // 탐색.
            Chasing,    // 추적.
            Attacking,  // 공격.
            Skilling,   // 스킬.
            Died,       // 사망.
        };
        public State state, currentState;
        //Monster Motion//
        public Animator animator;

        protected IAttackableBridge attackable;
        protected ISkillUsableBridge skillUsable;
        protected bool isInvincibility;

        public override void ReceiveDamage(float damage)
        {
            if (!isInvincibility)
            {
                int defencepossibility = Random.Range(1, 5);
                if (defencepossibility == 1)
                {
                    animator.SetTrigger(AniStateParm.Defence.ToString());

                }
                else
                {
                    animator.SetTrigger(AniStateParm.Hitted.ToString());
                    characterHealthPoint -= damage;

                    if (CharacterHealthPoint <= 0)
                    {
                        characterHealthPoint = 0;
                        ChangeState(State.Died);
                    }
                }
                StartCoroutine(AvoidAttac());
            }
        }
        //1초무적//
        protected IEnumerator AvoidAttac()
        {
            isInvincibility = true;
            yield return new WaitForSeconds(1.0f);
            isInvincibility = false;

        }


        public void ChangeState(State currentState)
        {
            this.currentState = currentState;
        }
        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }


        protected virtual void AttackTarget()
        {
            if (attackTarget != null)
            {
                attackable.Attack();
            }
        }
        protected virtual void UseSkill()
        {
            skillUsable.UseSkill();
        }

        protected void DropItem(MonsterType monsterType)
        {
            int itemCode = 0;

            if (monsterType == MonsterType.Normal)
            {

                itemCode = 1300 + Random.Range(11, 14);
            }

            else if (monsterType == MonsterType.Named)
            {

                itemCode = 1300 + Random.Range(21, 24);
            }
            else
            {
                itemCode = 1300 + Random.Range(31, 34);
            }
            if (GameControllManager.Instance.ObtainedItemDic.ContainsKey(itemCode))
                GameControllManager.Instance.ObtainedItemDic[itemCode]++;
            else
                GameControllManager.Instance.ObtainedItemDic.Add(itemCode, 1);

        }
        protected void Died()
        {
            GameControllManager.Instance.CheckGameOver();

            died = true;
            animator.SetTrigger(AniStateParm.Died.ToString());
            monsterMove.StopMove();

            int randomCount;
            switch (monsterType)
            {
                //5 , 10 , 20 //
                case MonsterType.Normal:
                    maxPercent = 21;
                    randomCount = Random.Range(1, maxPercent);
                    if (randomCount == maxPercent - 1)
                        DropItem(MonsterType.Normal);
                    break;
                case MonsterType.Named:
                    maxPercent = 11;
                    randomCount = Random.Range(1, maxPercent);
                    if (randomCount == maxPercent-1)
                        DropItem(MonsterType.Named);
                    break;
                case MonsterType.Boss:
                    maxPercent = 6;
                    randomCount = Random.Range(1, maxPercent-1);
                    if (randomCount == maxPercent)
                        DropItem(MonsterType.Boss);
                    break;
                default:
                    break;
            }

        }
        protected void RemovedFromWorld()
        {
            Destroy(gameObject);
        }

        protected void WalkAround()
        {
            //waitTime동안 대기
            if (waitTime > 0.0f)
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0.0f)
                {
                    Vector2 randomValue = Random.insideUnitCircle * walkRange;
                    // 이동할 곳을 설정한다.
                    Vector3 destinationPosition = startPosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                    animator.SetInteger(AniStateParm.Moving.ToString(), 1);

                    monsterMove.SetDestination(destinationPosition, speed);
                    monsterMove.SetDirection(destinationPosition);

                    waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
                }
            }
            if (attackTarget)
            {
                animator.SetInteger(AniStateParm.Battle.ToString(), 1);
                ChangeState(State.Chasing);
            }
        }

        protected void ChaseTarget()
        {
            //SetDestination to Player
            monsterMove.SetDestination(attackTarget.position, speed + 3);
            monsterMove.SetDirection(attackTarget.position);
            animator.SetInteger(AniStateParm.Moving.ToString(), 2);

            float attackRange = 3.0f;
            float skillRange = 10.0f;
            //스킬 사용할 조건
            float targetDistance = Vector3.Distance(attackTarget.position, transform.position);
            if (targetDistance <= skillRange && targetDistance > attackRange && !skillUse)
            {
                skillUse = true;
                monsterMove.SetDestination(attackTarget.position, 0);
                monsterMove.SetDirection(attackTarget.position);

                ChangeState(State.Skilling);

                StartCoroutine(WaitCoolTime());
            }
            else
            {
                if (targetDistance <= attackRange && !attacking)
                {
                    ChangeState(State.Attacking);
                    attacking = true;
                    animator.SetInteger(AniStateParm.Moving.ToString(), 0);
                }
            }
        }

        protected void AttackEnd()
        {
            if (animator.GetBool(AniStateParm.SkillOne.ToString()))
            {
                animator.SetBool(AniStateParm.SkillOne.ToString(), false);
            }
            else if (animator.GetBool(AniStateParm.SkillTwo.ToString()))
            {
                animator.SetBool(AniStateParm.SkillTwo.ToString(), false);
            }
            StartCoroutine(WaitNextState());


        }
        protected IEnumerator WaitNextState()
        {
            yield return new WaitForSeconds(1.5f);
            animator.SetInteger(AniStateParm.Attack.ToString(), 0);
            attacking = false;

            ChangeState(State.Chasing);
        }

        protected IEnumerator WaitCoolTime()
        {

            animator.SetInteger(AniStateParm.Moving.ToString(), 0);

            yield return new WaitForSeconds(skillCoolTime);
            skillUse = false;

        }

        protected void ChangeStateToWalking()
        {
            currentState = State.Walking;
            Debug.Log("aaa");
        }


    }
    
}
