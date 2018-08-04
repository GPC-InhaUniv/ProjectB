using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public abstract class BossState  {

    Boss boss;
    public Boss Boss
    {
        get { return boss; }
        set { boss = value; }
    }
    protected GameObject SkillPrefab;

    //protected ISkillUsable 
    public abstract void Attack();
    public abstract void Skill();


}
public class Phase1 : BossState
{
    public Phase1(Boss boss, GameObject skillPrefab)
    {
        Boss = boss;
        SkillPrefab = skillPrefab;

        Boss.attackable = new NormalAttack(Boss);

    }
    public override void Attack()
    {
        Boss.attackable.Attack();
    }

    public override void Skill()
    {
        Boss.skillUsable = new BossSkillFirst(Boss ,SkillPrefab);
    }
}
public class Phase2 : BossState
{

    public Phase2(Boss boss, GameObject skillPrefab)
    {
        Boss = boss;
        SkillPrefab = skillPrefab;

        Boss.attackable = new ComboAttack(Boss);

    }
    public override void Attack()
    {
        Boss.attackable = new ComboAttack(Boss);
    }

    public override void Skill()
    {
        throw new System.NotImplementedException();
    }
}

