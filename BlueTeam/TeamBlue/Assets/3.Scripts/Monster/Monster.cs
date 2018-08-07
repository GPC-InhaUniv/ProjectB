using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterAI
{
    public abstract class Monster : MonoBehaviour , IDamageInteractionable
    {
        // test //
        [SerializeField]
        protected AttackArea[] attackAreas;
        [SerializeField]
        protected GameObject skillprefab;
        //Monster Status//
        [SerializeField]
        protected int monsterHP, monsterMaxHP, walkRange ;
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
        [SerializeField]
        protected Transform attackTarget;
        [SerializeField]
        protected MonsterMove monsterMove;
        //Move To Destination//
        [SerializeField]
        protected Vector3 startPosition;

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

        public IAttackable Attackable;
        public ISkillUsable SkillUsable;

        public int MonsterPower;

        /// <summary>
        /// interface implement
        /// </summary>
        public IDamageInteractionable player;

        public void SendDamage(IDamageInteractionable target)
        {
           Test_Mediator.Instance.SendTarget(target, MonsterPower);
        }

        public void ReceiveDamage(int damage)
        {
            animator.SetTrigger("Hitted");
            monsterHP -= damage;
            if (monsterHP <= 0)
            {
                monsterHP = 0;
                ChangeState(State.Died);
            }
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
            Attackable.Attack();
        }
        protected virtual  void UseSkill()
        {
            SkillUsable.UseSkill();
        }

        protected void DropItem()
        {
            
            //if (dropItemPrefab.Length == 0) { return; }
            //GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
            //Instantiate(dropItem, transform.position, Quaternion.identity);
        }
        protected void Died()
        {
            died = true;
            animator.SetInteger("moving", 13);
            monsterMove.StopMove();
            DropItem();
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
                    animator.SetInteger("moving", 1);

                    monsterMove.SetDestination(destinationPosition, speed);
                    monsterMove.SetDirection(destinationPosition);

                    waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
                }
            }
            if (attackTarget)
            {
                animator.SetInteger("battle", 1);
                ChangeState(State.Chasing);
            }
        }

        protected void ChaseTarget()
        {
            //SetDestination to Player
            monsterMove.SetDestination(attackTarget.position, speed + 3);
            monsterMove.SetDirection(attackTarget.position);
            animator.SetInteger("moving", 2);

            float attackRange = 1.5f;
            float skillRange = 10.0f;
            //스킬 사용할 조건
            if (Vector3.Distance(attackTarget.position, transform.position) <= skillRange && !skillUse)
            {
                skillUse = true;
                monsterMove.SetDestination(attackTarget.position, 0);
                monsterMove.SetDirection(attackTarget.position);

                ChangeState(State.Skilling);

                StartCoroutine(WaitCoolTime());
            }
            else
            {
                if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
                {
                    ChangeState(State.Attacking);
                    animator.SetInteger("moving", 0);
                }
            }
        }

        protected void AttackEnd()
        {
            StartCoroutine(WaitNextState());
        }
        protected IEnumerator WaitNextState()
        {
            yield return new WaitForSeconds(0.5f);
            animator.SetInteger("Attack", 0);
            ChangeState(State.Chasing);
        }

        protected IEnumerator WaitCoolTime()
        {

            animator.SetInteger("moving", 0);

            yield return new WaitForSeconds(skillCoolTime);
            skillUse = false;

        }
    }
}





//// 일정거리안에 있으면 연속공격//
//protected void AttackCombo()
//{
//    float attackRange = 1.5f;
//    if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
//        animator.SetInteger("Attack", 2);
//    else
//    {
//        animator.SetInteger("Attack", 0);
//        ChangeState(State.Chasing);
//    }
//}