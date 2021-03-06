﻿using System.Collections;
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
    public enum State
    {
        Walking,
        Chasing,
        Attacking,
        Skilling,
        Died,
    }

    public delegate void NoticeDie(GameObject gameObject);
    public abstract class Monster : Character
    {
        public static event NoticeDie NoticeToRader;     
        public TestMonsterInfo testMonsterInfo;
        public Transform attackTarget;

        protected bool attacking, died, skillUse, isInvincibility;

        protected int stageLevel, walkRange;
        protected float skillCoolTime, waitTime, waitBaseTime, speed;

        protected MonsterType monsterType;
        protected IAttackableBridge attackable;
        protected ISkillUsableBridge skillUsable;
        protected State state, currentState;

        protected MonsterMove monsterMove;
        protected Vector3 startPosition;
        protected Animator animator;

        int randomPercent,maxPercent;

        [SerializeField]
        protected GameObject hitParticle;

        protected void SetMonsterInfo()
        {
            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;

            if (testMonsterInfo.TestCheck)
            {
                healthPoint = testMonsterInfo.MonsterMaxHP;
                walkRange = testMonsterInfo.WalkRange;
                attackPower = testMonsterInfo.AttackPower;
                skillCoolTime = testMonsterInfo.SkillCoolTime;
                waitTime= testMonsterInfo.WaitBaseTime;
                speed = testMonsterInfo.Speed;
            }
            else
            {
                stageLevel = GameControllManager.Instance.CurrentIndex;
                switch (monsterType)
                {
                    case MonsterType.Boss:
                        maxHealthPoint = 13000;
                        attackPower = 100;
                        break;
                    case MonsterType.Named:
                        maxHealthPoint = 1600;
                        attackPower = 120;
                        break;
                    case MonsterType.Normal:
                        maxHealthPoint = 1000;
                        attackPower = 70;
                        break;
                }
                ComputeStatus(stageLevel);
                healthPoint = maxHealthPoint;
                waitBaseTime = 2.0f;
                waitTime = waitBaseTime;
                walkRange = (monsterType == MonsterType.Boss) ? 30 : 15;
                skillCoolTime = 10;
                speed = 2;

                switch (monsterType)
                {
                    case MonsterType.Normal:
                        maxPercent = 20;
                        break;
                    case MonsterType.Named:
                        maxPercent = 11;
                        break;
                    case MonsterType.Boss:
                        maxPercent = 6;
                        break;
                    default:
                        break;
                }
                randomPercent = Random.Range(0, maxPercent);
                hitParticle.SetActive(false);
            }
        }

        protected void ComputeStatus(float stageLevel)
        {
            maxHealthPoint = maxHealthPoint * stageLevel;
            attackPower = attackPower * stageLevel;
        }

        public override void ReceiveDamage(float damage)
        {
            if (!died)
            {
                if (!isInvincibility)
                {
                    int defencepossibility = Random.Range(1, 5);
                    if (defencepossibility == 1)
                    {
                        animator.SetTrigger(AniStateParm.Defence.ToString());
                        SoundManager.Instance.SetSound(SoundFXType.EnemyDefence);
                    }
                    else
                    {
                        StartCoroutine(ShowHitEffect(1.0f));
                        healthPoint -= damage;
                        SoundManager.Instance.SetSound(SoundFXType.EnemyHit);
                        if (healthPoint <= 0)
                        {
                            healthPoint = 0;
                            ChangeState(State.Died);
                        }
                        else
                            animator.SetTrigger(AniStateParm.Hitted.ToString());
                    }
                    StartCoroutine(AvoidAttack());
                }
            }
        }

        protected IEnumerator AvoidAttack()
        {
            isInvincibility = true;
            yield return new WaitForSeconds(1.0f);
            isInvincibility = false;
        }

        protected IEnumerator ShowHitEffect(float time)
        {
            hitParticle.SetActive(true);
            yield return new WaitForSeconds(time);
            hitParticle.SetActive(false);
        }

        protected void Died()
        {
            died = true;
            monsterMove.StopMove();
            if (monsterType == MonsterType.Boss)
                SoundManager.Instance.SetSound(SoundFXType.BossDeath);
            else
                SoundManager.Instance.SetSound(SoundFXType.EnemyDeath);

            animator.SetTrigger(AniStateParm.Died.ToString());
            GameControllManager.Instance.CheckGameClear();

            if (randomPercent == maxPercent) 
                DropItem(MonsterType.Normal);
            NoticeToRader(gameObject);
        }

        protected void ChangeState(State currentState)
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
                SoundManager.Instance.SetSound(SoundFXType.EnemyAttack);
            }
        }
        protected virtual void UseSkill()
        {
            skillUsable.UseSkill();
        }

        protected void DropItem(MonsterType monsterType)
        {
            int itemCode = 0;
            int equipmentItemNum = 1300;

            if (monsterType == MonsterType.Normal)
                itemCode = equipmentItemNum + Random.Range(11, 14);
            else if (monsterType == MonsterType.Named)
                itemCode = equipmentItemNum + Random.Range(21, 24);
            else
                itemCode = equipmentItemNum + Random.Range(31, 34);

            if (GameControllManager.Instance.ObtainedItemDic.ContainsKey(itemCode))
                GameControllManager.Instance.ObtainedItemDic[itemCode]++;
            else
                GameControllManager.Instance.ObtainedItemDic.Add(itemCode, 1);
        }

        protected void RemovedFromWorld()
        {
            gameObject.SetActive(false);
        }

        protected void WalkAround()
        {
            if (waitTime > 0.0f)
            {
                waitTime -= Time.deltaTime;
                if (waitTime <= 0.0f)
                {
                    Vector2 randomValue = Random.insideUnitCircle * walkRange;
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
            monsterMove.SetDestination(attackTarget.position, speed + 3);
            monsterMove.SetDirection(attackTarget.position);
            animator.SetInteger(AniStateParm.Moving.ToString(), 2);

            float attackRange = 3.0f;
            float skillRange = 10.0f;

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
        }
    }
}
