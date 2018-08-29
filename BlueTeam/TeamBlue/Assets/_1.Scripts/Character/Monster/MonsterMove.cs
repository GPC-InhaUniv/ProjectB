using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class MonsterMove : MonoBehaviour
    {
        const float STOPPINGDISTANCE = 4.0f;
        const float ROTATIONSPEED = 100.0f;

        bool arrived;
        bool forceRotate;

        float walkSpeed;

        Vector3 destination, forceRotateDirection;
        Rigidbody rigidMove;

        void Start()
        {
            destination = transform.position;
            rigidMove = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            if (!arrived)
            {
                Vector3 finaldestination = destination;
                finaldestination.y = transform.position.y;

                Vector3 direction = (finaldestination - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, finaldestination);

                if (arrived || distance < STOPPINGDISTANCE)
                    arrived = true;

                //MovePosition//
                rigidMove.MovePosition(Vector3.MoveTowards(transform.position, finaldestination, walkSpeed * Time.fixedDeltaTime));
                //MoveRotation//
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
            arrived = true;
        }
    }
}
