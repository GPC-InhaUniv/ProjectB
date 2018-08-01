using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//나중에 I  + able 지우기//
public abstract class IAttackable
{


    protected Animator anim;

    public abstract void Attack(Animator anim);


}
public class NormalAttack : IAttackable
{

    public override void Attack(Animator anim)
    {
     //   this.anim = anim;


        anim.SetInteger("Attack", 2);
    }

}
public class ComboAttack : IAttackable
{
    public override void Attack(Animator anim)
    {
   //     this.anim = anim;
        anim.SetInteger("Attack", 1);

    }

}

