using UnityEngine;

namespace ProjectB.Characters.Players
{


    public abstract class PlayerCharacterState
    {
        protected PlayerAnimation playerAinmaton;

        protected Rigidbody playerRigidbody;

        protected Transform playerTransform;

        protected Collider playerCollider;

        protected Vector3 targetVector;

        public PlayerCharacterState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider,  Vector3 targetVector)
        {
            this.playerAinmaton = playerAinmaton;
            this.playerRigidbody = playerRigidbody;
            this.playerTransform = playerTransform;
            this.playerCollider = playerCollider;
            this.targetVector = targetVector;
        }

        public abstract void Tick(Vector3 moveVector);
    }

    public class PlayerCharacterIdleState : PlayerCharacterState
    {
        public PlayerCharacterIdleState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider,  Vector3 targetVector) 
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            playerCollider.enabled = true;
        }
    }

    public class PlayerCharacterAttackState : PlayerCharacterState
    {
        public PlayerCharacterAttackState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider,  Vector3 targetVector)
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            //playerAinmaton.AttackAnimation(AnimationState.Attack.ToString() + AttackNumber.ToString());
        }
    }

    public class PlayerCharacterSkillState : PlayerCharacterState
    {
        public PlayerCharacterSkillState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider, Vector3 targetVector)
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            playerAinmaton.SkillAnimation(AnimationState.Skill.ToString());
        }
    }

    public class PlayerCharacterRunState : PlayerCharacterState
    {
        public PlayerCharacterRunState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider, Vector3 targetVector)
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            playerRigidbody.velocity = moveVector * 900 * Time.deltaTime;
            playerTransform.rotation = Quaternion.LookRotation(moveVector);
        }
    }

    public class PlayerCharacterBackStepState : PlayerCharacterState
    {
        public PlayerCharacterBackStepState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider,Vector3 targetVector)
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            playerCollider.enabled = false;
            playerAinmaton.BackStepAnimation();
            playerRigidbody.velocity = -targetVector * 3000 * Time.deltaTime;
        }
    }

    public class PlayerCharacterDieState : PlayerCharacterState
    {
        public PlayerCharacterDieState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform, Collider playerCollider,  Vector3 targetVector)
        : base(playerAinmaton, playerRigidbody, playerTransform, playerCollider, targetVector)
        {

        }

        public override void Tick(Vector3 moveVector)
        {
            playerCollider.enabled = false;
            playerAinmaton.DieAnimation();
        }
    }
}


