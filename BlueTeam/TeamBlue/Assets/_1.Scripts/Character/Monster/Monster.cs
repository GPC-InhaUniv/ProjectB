using System.Collections;
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
    public delegate void NoticeDie(GameObject gameObject);

    public abstract class Monster : Character
    {
        // Monster State//
        protected enum State
        {
            Walking,    // 탐색.
            Chasing,    // 추적.
            Attacking,  // 공격.
            Skilling,   // 스킬.
            Died,       // 사망.
        };

        public static event NoticeDie NoticeToRader;
        public Transform attackTarget;

        protected bool attacking, died, skillUse;
        protected bool isInvincibility;

        //Monster Status//
        protected int walkRange;
        protected float skillCoolTime;
        //Monster System//
        protected int stageLevel;
        protected float waitBaseTime;
        protected float waitTime, speed;

        protected MonsterType monsterType;
        protected IAttackableBridge attackable;
        protected ISkillUsableBridge skillUsable;
        protected State state, currentState;

        protected MonsterMove monsterMove;
        protected Vector3 startPosition;
        protected Animator animator;

        int maxPercent;

        [SerializeField]
        protected GameObject hitParticle;

        protected void SetMonsterInfo()
        {
            monsterMove = GetComponent<MonsterMove>();
            animator = GetComponent<Animator>();
            startPosition = transform.position;

            stageLevel = GameControllManager.Instance.CurrentIndex;
            float levelOne = 1.0f;
            float levelTwo = 1.5f;
            float levelThree = 2.0f;
            switch (stageLevel)
            {
                case 1:
                    maxHealthPoint = maxHealthPoint * levelOne;
                    healthPoint = maxHealthPoint;
                    attackPower = attackPower * levelOne;
                    break;
                case 2:
                    maxHealthPoint = maxHealthPoint * levelTwo;
                    healthPoint = maxHealthPoint;
                    attackPower = attackPower * levelTwo;
                    break;
                case 3:
                    maxHealthPoint = maxHealthPoint * levelThree;
                    healthPoint = maxHealthPoint;

                    attackPower = attackPower * levelThree;
                    break;
            }
            //stageLevel에 따라 조절 예정//
            waitBaseTime = levelThree;
            waitTime = waitBaseTime;

            int range = 15;
            int coolTime = 10;
            int speed = 2;
            if (monsterType == MonsterType.Boss)
            {
                walkRange = range * 2;
                skillCoolTime = coolTime;
                this.speed = speed;
            }
            else
            {
                walkRange = range;
                skillCoolTime = coolTime;
                this.speed = speed;
            }
            hitParticle.SetActive(false);
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
                    if (randomCount == maxPercent - 1)
                        DropItem(MonsterType.Named);
                    break;
                case MonsterType.Boss:
                    maxPercent = 6;
                    randomCount = Random.Range(1, maxPercent - 1);
                    if (randomCount == maxPercent)
                        DropItem(MonsterType.Boss);
                    break;
                default:
                    break;
            }
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

//            START DEBUG CHECK
//            Debug.Log("몬스터무브 " +monsterMove);
//            Debug.Log("애니메이터 " +animator);
//            Debug.Log("스타트포지션 " +startPosition);
//            Debug.Log("대기시간 " +waitTime);
//            Debug.Log("몬스터타입 "+monsterType);
//            Debug.Log("몬스터HP "+healthPoint);
//            Debug.Log("공격력 "+attackPower);
//            Debug.Log("워크레인지 "+walkRange);
//            Debug.Log("쿨타임 "+skillCoolTime);
//            Debug.Log("스피드 "+this.speed);
