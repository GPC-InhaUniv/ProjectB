using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Character.Monster;


public class MultiAttackRangeSkill : ISkillUsable {

    Player player = null;

	public MultiAttackRangeSkill(Player player)
    {
        this.player = player;
    }

    public void UseSkill()
    {
    }
}
