using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProjectB.Characters.Monsters
{
    [Serializable]
    public struct TestMonsterInfo
    {
        public bool TestCheck;
        public int MonsterMaxHP, WalkRange, AttackPower;
        public float SkillCoolTime, WaitBaseTime, Speed;
    }
}