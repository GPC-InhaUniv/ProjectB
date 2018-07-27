using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonsterAI
{
    //애니메이션 콤보 공격  Attack 불 값 , Integer 값 수정중//

    public abstract class Monster : MonoBehaviour
    {

        //Monster Status//
        [SerializeField]
        protected int monsterHP, monsterMaxHP, monsterPower, walkRange;
        //Monster System//
        [SerializeField]
        protected float waitBaseTime = 2.0f;
        [SerializeField]
        protected float waitTime;
        //Set Target//
        [SerializeField]
        protected Transform attackTarget;
        [SerializeField]
        protected bool isCoroutineRunning;
        //Monster Motion//
        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected MonsterMove monsterMove;
        [SerializeField]
        protected bool attacking, died, skillUse;
        [SerializeField]
        protected float speed;

        //Move To Destination//
        [SerializeField]
        protected Vector3 startPosition;
        protected Vector3 endPosition;
        // Monster State//
        protected enum State
        {
            //코루틴//
            Walking,    // 탐색.
                        //업데이트//
            Chasing,    // 추적.
            Attacking,  // 공격.
            Skilling,   // 스킬.
                        //코루틴//
            Died,       // 사망.
        };

        [SerializeField]
        protected State state, currentState;

        private void Start()
        {
            monsterMove = GetComponent<MonsterMove>();
            //animator = GetComponent<Animator>();
            startPosition = transform.position;

        }

        private void Update()
        {
            switch (state)
            {
                case State.Walking:
                    Walkaround();
                    Debug.Log("ggo");
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
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
            if (state != currentState)
            {
                state = currentState;
                switch (state)
                {
                    case State.Walking:
                        Walkaround();
                        Debug.Log("ggo");
                        break;
                    case State.Chasing:
                        ChaseTarget();
                        break;
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

        }
        protected void ChangeState(State currentState)
        {
            this.currentState = currentState;
        }
        public void SetAttackTarget(Transform target)
        {
            attackTarget = target;
        }


        protected abstract void AttackTarget();
        protected abstract void UseSkill();

        protected void Damaged()
        {

        }
        protected void Died()
        {

        }
        protected void DropItem()
        {

        }
        protected void SetTarget()
        {

        }


        protected void Walkaround()
        {

            // 대기 시간이 아직 남았다면.
            if (waitTime > 0.0f)
            {
                // 대기 시간을 줄인다.
                waitTime -= Time.deltaTime;
                // 대기 시간이 없어지면.
                if (waitTime <= 0.0f)
                {
                    Vector2 randomValue = Random.insideUnitCircle * walkRange;
                    // 이동할 곳을 설정한다.
                    Vector3 destinationPosition = startPosition + new Vector3(randomValue.x, 0.0f, randomValue.y);

                    animator.SetInteger("moving", 1);

                    monsterMove.SetDestination(destinationPosition, speed);
                    monsterMove.SetDirection(destinationPosition);

                    waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
                    //animator.SetInteger("moving", 0);
                    Debug.Log("walkaround running");
                }
            }

            // 목적지에 도착한다.
            //if (characterMove.Arrived())
            //{
            //    // 대기 상태로 전환한다.
            //    waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            //}
            // 타겟을 발견하면 추적한다.
            if (attackTarget)
            {
                Debug.Log("왜안나오냐");
                animator.SetInteger("battle", 1);

                ChangeState(State.Chasing);
                Debug.Log("attacktarget on");
            }





        }
        protected void ChaseTarget()
        {
            //SetDestination to Player
            monsterMove.SetDestination(attackTarget.position, speed + 3);
            monsterMove.SetDirection(attackTarget.position);

            animator.SetInteger("moving", 2);

            Debug.Log(attackTarget.position);
            // 2미터 이내로 접근하면 공격한다.
            float attackRange = 1.5f;
            if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
            {
                ChangeState(State.Attacking);
                animator.SetInteger("moving", 0);

            }
        }

        protected void EndAttack()
        {
            //ChangeState(State.Chasing);

            float attackRange = 2.0f;

            Debug.Log(Vector3.Distance(attackTarget.position, transform.position));
            if (Vector3.Distance(attackTarget.position, transform.position) <= attackRange)
            {
                animator.SetInteger("Attack", 2);

                Debug.Log(attackRange);

            }
            else
            {
                animator.SetInteger("Attack", 0);

                ChangeState(State.Chasing);
                Debug.Log("EndAttack");
            }
        }
        protected void ComboAttack()
        {

        }
    }
}