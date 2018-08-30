using UnityEngine;
using ProjectB.Utility;

namespace ProjectB.Characters.Players
{
    public abstract class PlayerCharacterState
    {
        protected PlayerAnimation playerAinmaton;

        protected Rigidbody playerRigidbody;

        protected Transform playerTransform;

        protected float moveSpeed = 60.0f;
        protected float backStepSpeed = 600.0f;

        public PlayerCharacterState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        {
            this.playerAinmaton = playerAinmaton;
            this.playerRigidbody = playerRigidbody;
            this.playerTransform = playerTransform;
        }

        public abstract void Tick(Vector3 moveVector, bool isState);
    }

    public class PlayerCharacterIdleState : PlayerCharacterState
    {
        public PlayerCharacterIdleState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform) 
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState) { }
    }

    public class PlayerCharacterAttackState : PlayerCharacterState
    {
        public PlayerCharacterAttackState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState) { }
    }

    public class PlayerCharacterSkillState : PlayerCharacterState
    {
        public PlayerCharacterSkillState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState)
        {
            playerAinmaton.SkillAnimation();
        }
    }

    public class PlayerCharacterRunState : PlayerCharacterState
    {
        public PlayerCharacterRunState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState)
        {
            playerAinmaton.RunAnimation(isAnimState);
            playerRigidbody.AddForce(moveVector * moveSpeed, ForceMode.Impulse);
            playerTransform.rotation = Quaternion.LookRotation(moveVector);
        }
    }

    public class PlayerCharacterBackStepState : PlayerCharacterState
    {
        public PlayerCharacterBackStepState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState)
        {
            playerRigidbody.AddForce(-moveVector * backStepSpeed, ForceMode.Impulse);
            playerAinmaton.BackStepAnimation();
        }
    }

    public class PlayerCharacterDieState : PlayerCharacterState
    {
        public PlayerCharacterDieState(PlayerAnimation playerAinmaton, Rigidbody playerRigidbody, Transform playerTransform)
        : base(playerAinmaton, playerRigidbody, playerTransform) { }

        public override void Tick(Vector3 moveVector, bool isAnimState)
        {
            playerAinmaton.DieAnimation();        
        }
    }
}


