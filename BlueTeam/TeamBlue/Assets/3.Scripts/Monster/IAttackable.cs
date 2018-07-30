using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable  {

    void Attack();


}
public class NormalAttack : IAttackable
{
    public void Attack()
    {
        throw new System.NotImplementedException();

    }
}

public class StrunAttack : IAttackable
{
    public void Attack()
    {
        throw new System.NotImplementedException();
    }
}