﻿namespace ProjectB.Characters.Monsters
{
    public class Normal : Monster
    {
        void Start()
        {
            monsterType = MonsterType.Normal;
            SetMonsterInfo();
            attackable = new NormalAttack(animator);
            skillUsable = new NoSkill(animator);
        }

        void Update()
        {
            switch (state)
            {
                case State.Walking:
                    WalkAround();
                    break;
                case State.Chasing:
                    ChaseTarget();
                    break;
            }
            if (state != currentState)
            {
                state = currentState;
                switch (state)
                {
                    case State.Attacking:
                        AttackTarget();
                        break;
                    case State.Skilling:
                        NoSkill.SetState += ChangeStateToWalking;
                        UseSkill();
                        NoSkill.SetState -= ChangeStateToWalking;
                        break;
                    case State.Died:
                        Died();
                        break;
                }
            }
        }
    }
}