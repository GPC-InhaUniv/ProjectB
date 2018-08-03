using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

public interface ISkillUsable
{


    //    Monster Monster { get; set; }

    void UseSkill();

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

    public void UseSkill()
    {
        Monster.ChangeState(Monster.State.Chasing);
    }

}
public class NamedSkill : ISkillUsable
{
    Monster monster;
    public Monster Monster
    {
        get { return monster; }
        set { monster = value; }
    }
    GameObject skillPrefab;

    public NamedSkill(Monster monster, GameObject skillPrefab)
    {
        Monster = monster;
        this.skillPrefab = skillPrefab;
    }

    public void UseSkill()
    {
        Debug.Log(Monster.transform.position);
        //(공격,소환)스킬 오브젝트 풀에서 받아와서 사용할 예정//
        
        skillPrefab.transform.position = Monster.transform.position;
        skillPrefab.SetActive(true);
        Monster.animator.SetInteger("Attack", 3);
            ///anim.SetInteger("Attack", 3);

        
    }
    
}
public class BossSkillFirst : ISkillUsable
{
    //public Monster Monster
    //{
    //    get { return Monster; }
    //    set { Monster = value; }
    //}
    public BossSkillFirst(Boss boss, GameObject skillPrefab)
    {
        

    }
    public void UseSkill()
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

    public void UseSkill()
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
    public void UseSkill()
    {


    }
}