using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterAI;

//나중에 I  + able 지우기//
public interface  IAttackable
{

     Monster Monster { get; set; }

    void Attack();


}
public class NormalAttack : IAttackable
{
    Monster monster;
    public Monster Monster
    {
        get { return monster; }
        set { monster = value; }
    }
    public NormalAttack(Monster monster)
    {
        Monster = monster;
    }

    public  void Attack()
    {
        Monster.animator.SetInteger("Attack", 2);
    }

}
public class ComboAttack : IAttackable 
{
    Monster monster;
    public Monster Monster
    {
        get { return monster; }
        set { monster = value; }
    }
    public ComboAttack(Monster monster)
    {
        Monster = monster;
    }
    public  void Attack()
    {
        Monster.animator.SetInteger("Attack", 1);
    }


}

