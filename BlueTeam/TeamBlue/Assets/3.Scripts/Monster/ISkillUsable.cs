using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillUsable  {

    void UseSkill();

}

public class NoSkill : ISkillUsable
{
    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
public class NamedSkill : ISkillUsable
{
    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
public class BossSkillFirst : ISkillUsable
{
    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
public class BossSkillSecond : ISkillUsable
{
    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}
public class BossSkillThird : ISkillUsable
{
    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }
}