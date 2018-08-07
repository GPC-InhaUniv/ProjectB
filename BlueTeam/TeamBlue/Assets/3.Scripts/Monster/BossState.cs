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

        Boss.Attackable = new NormalAttack(Boss);
        Boss.SkillUsable = new BossSkillFirst(Boss, SkillPrefab);


    }
    public override void Attack()
    {
        Boss.Attackable.Attack();
    }

    public override void UseSkill()
    {
        Boss.SkillUsable.UseSkill();
    }
}
public class Phase2 : BossState
{
    public Phase2(Boss boss, GameObject skillPrefab)
    {
        Boss = boss;
        SkillPrefab = skillPrefab;

        Boss.Attackable = new ComboAttack(Boss);
        Boss.SkillUsable = new BossSkillFirst(Boss, SkillPrefab);

    }
    public override void Attack()
    {
        Boss.Attackable.Attack();
    }
    public override void UseSkill()
    {
        Boss.SkillUsable.UseSkill();

    }
}

