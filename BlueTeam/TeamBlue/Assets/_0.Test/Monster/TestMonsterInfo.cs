using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TestMonsterInfo {


    // test //
    [SerializeField]
    public AttackArea[] attackAreas;
    [SerializeField]
    public GameObject skillprefab;
    //Monster Status//
    [SerializeField]
    public int monsterHP, monsterMaxHP, walkRange;
    [SerializeField]
    public float skillCoolTime;
    [SerializeField]
    public bool attacking, died, skillUse;
    [SerializeField]
    public GameObject[] dropItemPrefab;
    //Monster System//
    [SerializeField]
    public float waitBaseTime;
    [SerializeField]
    public float waitTime, speed;
    //Set Target//
    [SerializeField]
    public Transform attackTarget;
    [SerializeField]
    public MonsterMove monsterMove;
    //Move To Destination//
    [SerializeField]
    public Vector3 startPosition;

}
