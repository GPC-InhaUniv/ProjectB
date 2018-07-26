using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{

    /// <summary>
    /// Idle상태 새로 만들고 , 유니드라에는 IDle상태가 없음 
    /// Attacking  state패턴으로 3 개로 나누기
    /// </summary>


    //Monster Status//
    [SerializeField]
    protected int monsterHP, monsterMaxHP, monsterPower, walkRange;
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
    //State state = State.Walking;        // 현재 스테이트.
    //State nextState = State.Walking;	// 다음 스테이트.
    [SerializeField]
    protected State state;

    private void Start()
    {
        monsterMove = GetComponent<MonsterMove>();
        //animator = GetComponent<Animator>();
        startPosition = transform.position;
        state = State.Walking;

    }
    private void FixedUpdate()
    {

    }

    private void Update()
    {
        switch (state)
        {
            case State.Walking:
                StartCoroutine(Walk());
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
    protected void ChangeState(State state)
    {
        this.state = state;
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


    protected IEnumerator Walk()
    {
        isCoroutineRunning = true;
        if (attackTarget)
        {
            animator.SetInteger("battle", 1);

            ChangeState(State.Chasing);
            Debug.Log("attacktarget on");
            yield return null;
        }
        else
        {
            Vector2 randomValue = Random.insideUnitCircle * walkRange;
            // 이동할 곳을 설정한다.
            Vector3 destinationPosition = startPosition + new Vector3(randomValue.x, 0.0f, randomValue.y);

            animator.SetInteger("moving", 1);

            monsterMove.SetDestination(destinationPosition, speed);
            monsterMove.SetDirection(destinationPosition);


            const float IDELTIME = 2f;

            yield return new WaitForSeconds(IDELTIME);


            //float distance = Vector3.Distance(transform.position, destinationPosition);
            //if (distance <= 1.5)
            animator.SetInteger("moving", 0);

            //StartCoroutine(Waliking());
            //state = State.Skilling; //상속받은 오브젝트도 스킬링으로 바뀌는거 확인
        }
        isCoroutineRunning = false;
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

    protected void Endattack()
    {
        ChangeState(State.Chasing);
        Debug.Log("EndAttack");
    }

}