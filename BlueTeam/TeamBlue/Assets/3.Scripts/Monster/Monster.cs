using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterAI
{

    //공격 1번 공격2번 한번에 같은 취소로 하기//
    public abstract class Monster : MonoBehaviour
    {
        // test //
        [SerializeField]
        protected AttackArea[] attackAreas;
        // Monster State//
        protected enum State
        {
            Walking,    // 탐색.
            Chasing,    // 추적.
            Attacking,  // 공격.
            Skilling,   // 스킬.
            Died,       // 사망.
        };
        [SerializeField]
        protected State state, currentState;
        //Monster Status//
        [SerializeField]
        protected int monsterHP, monsterMaxHP, walkRange;
        [SerializeField]
        protected bool attacking, died, skillUse;
        protected GameObject[] dropItemPrefab;
        //Monster System//
        [SerializeField]
        protected float waitBaseTime;
        [SerializeField]
        protected float waitTime, speed;
        //Set Target//
        [SerializeField]
        protected Transform attackTarget;
        //Monster Motion//
        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected MonsterMove monsterMove;
        //Move To Destination//
        [SerializeField]
        protected Vector3 startPosition;
        protected IAttackable attackable;
        protected ISkillUsable skillUsable;

        public int MonsterPower;



        //private void Start()
        //{
        //    monsterMove = GetComponent<MonsterMove>();
        //    //animator = GetComponent<Animator>();
        //    startPosition = transform.position;

        //}

        protected void ChangeState(State currentState)
        {
            this.currentState = currentState;
        }
        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }


        protected  void AttackTarget()
        {
          //  animator.SetInteger("Attack", 1);
            attackable.Attack(animator);
        }
        protected  void UseSkill()
        {
            skillUsable.UseSkill();
        }

        protected void Damaged(int damage)
        {

            monsterHP -= damage;
            if (monsterHP <= 0)
            {
                monsterHP = 0;
                ChangeState(State.Died);
            }
        }
        protected void DropItem()
        {
            //if (dropItemPrefab.Length == 0) { return; }
            //GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
            //Instantiate(dropItem, transform.position, Quaternion.identity);
        }
        protected void Died()
        {
            //ChangeState(State.Died);
            died = true;
            animator.SetInteger("moving", 13);
            monsterMove.StopMove();
            DropItem();
        }
        protected void RemovedFromWorld()
        {
            Destroy(gameObject);
        }

        protected void Walkaround()
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

            // 1.5미터 이내로 접근하면 공격
            float attackRange = 1.5f;
            if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
            {
                ChangeState(State.Attacking);
                animator.SetInteger("moving", 0);
            }
        }

        protected void AttackCombo()
        {
            float attackRange = 1.5f;
            if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
                animator.SetInteger("Attack", 2);
            else
            {
                animator.SetInteger("Attack", 0);
                ChangeState(State.Chasing);
            }
        }
        protected void AttackEnd()
        {
            //애니메이터 파라미터값 확인//
           // if(animator.parameters==true)
            StartCoroutine(WaitNextState());
        }
        protected IEnumerator WaitNextState()
        {
            yield return new WaitForSeconds(0.5f);
            animator.SetInteger("Attack", 0);
            animator.SetBool("Combo", false);

            ChangeState(State.Chasing);
            Debug.Log("gogogo");
        }
    }
}