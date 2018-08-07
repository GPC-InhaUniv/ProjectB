using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public abstract class BossState  {

    Boss boss;
    public Boss Boss
    {
        get { return boss; }
        set { boss = value; }
    }
    protected GameObject SkillPrefab;

    public abstract void Attack();
    public abstract void UseSkill();


}
public class Phase1 : BossState
{
    public Phase1(Boss boss, GameObject skillPrefab)
    {
        Boss = boss;
        SkillPrefab = skillPrefab;

        Boss.attackable = new NormalAttack(Boss);
        Boss.skillUsable = new BossSkillFirst(Boss, SkillPrefab);


    }
    public override void Attack()
    {
        Boss.attackable.Attack();
    }

    public override void UseSkill()
    {
        Boss.skillUsable.UseSkill();
    }
}
public class Phase2 : BossState
{
    public Phase2(Boss boss, GameObject skillPrefab)
    {
        Boss = boss;
        SkillPrefab = skillPrefab;

        Boss.attackable = new ComboAttack(Boss);
        Boss.skillUsable = new BossSkillFirst(Boss, SkillPrefab);

    }
    public override void Attack()
    {
        Boss.attackable.Attack();
    }
    public override void UseSkill()
    {
        Boss.skillUsable.UseSkill();

    }
}

