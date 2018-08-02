using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

public interface ISkillUsable  {


//    Monster Monster { get; set; }

    void UseSkill(GameObject monster , Animator anim);

}

public class NoSkill : ISkillUsable
{
    //Monster monster;
    //public Monster Monster
    //{
    //    get { return monster; }
    //    set { monster = value; }
    //}
    public void UseSkill(GameObject monster, Animator anim)
    {
        //Monster.ChangeState(Monster.State.Chasing);

        anim.SetInteger("Attack", 3);


        
          
    }

}
public class NamedSkill : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}

    public void UseSkill(GameObject monster, Animator anim)
    {

    }
}
public class BossSkillFirst : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}

    public void UseSkill(GameObject monster, Animator anim)
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

    public void UseSkill(GameObject monster, Animator anim)
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
    public void UseSkill(GameObject monster, Animator anim)
    {


    }
}