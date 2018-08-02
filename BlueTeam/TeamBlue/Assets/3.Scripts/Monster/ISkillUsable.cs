using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

public interface ISkillUsable
{


    //    Monster Monster { get; set; }

    void UseSkill(Animator anim);

}

public class NoSkill : ISkillUsable
{
    Monster monster;
    public Monster Monster
    {
        get { return monster; }
        set { monster = value; }
    }
    public NoSkill(Monster monster)
    {
        Monster = monster;
    }

    public void UseSkill(Animator anim)
    {

        this.monster.ChangeState(Monster.State.Chasing);
    }

}
public class NamedSkill : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}

    public void UseSkill(Animator anim)
    {
        anim.SetInteger("Attack", 3);

    }
}
public class BossSkillFirst : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}

    public void UseSkill(Animator anim)
    {


    }
}
public class BossSkillSecond : ISkillUsable
{
    public Monster Monster
    {
        get { return Monster; }
        set { Monster = value; }
    }

    public void UseSkill(Animator anim)
    {


    }
}
public class BossSkillThird : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}
    public void UseSkill(Animator anim)
    {


    }
}