using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//나중에 I  + able 지우기//
public abstract class IAttackable  {


    protected Animator anim;

    public abstract void Attack(Animator anim);

    public abstract void AttackEnd();


    protected Collider[] activeColliders;
}
public class NormalAttack : IAttackable
{

    public override void Attack(Animator anim)
    {
        this.anim = anim;


        anim.SetInteger("Attack", 1);
    }

    public override void AttackEnd()
    {

    }
}
public class ComboAttack : IAttackable
{
    public override void Attack(Animator anim)
    {
        this.anim = anim;
        anim.SetInteger("Attack", 1);
        anim.SetBool("Combo", true);

    }

    public override void AttackEnd()
    {
        throw new System.NotImplementedException();
    }
}

