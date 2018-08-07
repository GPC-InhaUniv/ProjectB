using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAttackRangeSkill : ISkillUsable {

    Player player = null;

	public MultiAttackRangeSkill(Player player)
    {
        this.player = player;
    }

    public void UseSkill()
    {
        player.SkillNumber = 1;
        //this.player.SetState(new PlayerCharacterAttackState(player));
    }
}
