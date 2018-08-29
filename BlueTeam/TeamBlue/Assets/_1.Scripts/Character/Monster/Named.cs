namespace ProjectB.Characters.Monsters
{
    public class Named : Monster
    {
        void Start()
        {
            monsterType = MonsterType.Named;
            SetMonsterInfo();
            attackable = new ComboAttack(animator);
            skillUsable = new NamedSkill(animator);
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
                        UseSkill();
                        break;
                    case State.Died:
                        Died();
                        break;
                }
            }
        }
    }
}
