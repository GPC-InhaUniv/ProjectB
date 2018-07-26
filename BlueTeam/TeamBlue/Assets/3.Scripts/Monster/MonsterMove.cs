using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {

    const float STOPPINGDISTANCE = 2.0f;
    const float ROTATIONSPEED = 100.0f;

    //Go To Destination//
    [SerializeField]
    bool arrived = false;
    [SerializeField]
    bool forceRotate = false;
    [SerializeField]
    Vector3 destination, forceRotateDirection;
    [SerializeField]
    float walkSpeed;
    [SerializeField]
    Rigidbody rigidMove;

    // Use this for initialization
    void Start()
    {
        destination = transform.position;
        rigidMove = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!arrived)
        {
            Vector3 finaldestination = destination;
            //Same position.y value//
            finaldestination.y = transform.position.y;
            //Set direction//
            Vector3 direction = (finaldestination - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, finaldestination);


            if (arrived || distance < STOPPINGDISTANCE)
            {
                arrived = true;
            }
            rigidMove.MovePosition(Vector3.MoveTowards(transform.position, finaldestination, walkSpeed * Time.fixedDeltaTime));

            Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
            rigidMove.MoveRotation(Quaternion.Lerp(transform.rotation, characterTargetRotation, ROTATIONSPEED * Time.deltaTime));




        }

        if (forceRotate && Vector3.Dot(transform.forward, forceRotateDirection) > 0.99f)
            forceRotate = false;
    }
    public void SetDestination(Vector3 destination, float speed)
    {
        walkSpeed = speed;
        arrived = false;
        this.destination = destination;
    }

    public void SetDirection(Vector3 direction)
    {
        forceRotateDirection = direction;
        forceRotateDirection.y = 0;
        forceRotateDirection.Normalize();
        forceRotate = true;
    }

    public void StopMove()
    {
        destination = transform.position;
    }

    // 목적지에 도착했는지 조사한다(도착했다 true / 도착하지 않았다 false).
    public bool Arrived()
    {
        return arrived;
    }


}

