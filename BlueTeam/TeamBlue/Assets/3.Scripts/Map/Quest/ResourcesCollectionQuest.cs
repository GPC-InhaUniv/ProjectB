using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퀘스트 마지막, 수집할 자원

abstract public class ResourcesCollectionQuest : IQuestViable
{
    protected bool isProgress;          // 퀘스트를 수락했는지 안헀는지
    protected bool lastQuest;           //
    protected int CollectionResources;  

    abstract public string AcceptToQuest();                         //퀘스트를 수락한다.
    abstract public string ShowContentsOfQuest();                   //퀘스트 내용을 보여주다.
    abstract public string ProceedToQuest(MonsterType monsterType); //퀘스트를 진행한다.
    abstract public void CompleteToQuest();
}