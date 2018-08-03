using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BVillageQuest : HuntingQuest
{
    public BVillageQuest()
    {
        lastQuest = 0; // GameManager
        isProgress = false;
        questStateType = QuestStateType.수락;
    }

    public override string AcceptToQuest()
    {
        if (lastQuest < maxQuest)
        {
            if (questStateType == QuestStateType.수락)
            {
                questStateType = QuestStateType.진행;
                isProgress = true;
            }
            else if (questStateType == QuestStateType.완료)
            {
                questStateType = QuestStateType.수락;
                Debug.Log("보상 : B마을 우호도 1, 경험치 500, 발전카드 1장");
            }
        }
        else
        {
            questStateType = QuestStateType.완료;
        }
        return questStateType.ToString();
    }

    public override string ShowContentsOfQuest()
    {
        string QuestContents = "";
        string MonsterName = "";
        assignmentMonster = defaultMonsterCount + additionMonsterCount;

        if (lastQuest < maxQuest)
        {
            if (lastQuest % 2 != 1)
            {
                MonsterName = "돌";
            }
            else if (lastQuest % 2 != 0)
            {
                MonsterName = "철광";
            }
            QuestContents = "B마을 퀘스트\n" + MonsterName + " 몬스터를" + assignmentMonster + "마리 처치";
        }
        else
        {
            QuestContents = "B마을 퀘스트\n퀘스트 모두 완료";
        }
        return QuestContents;
    }


    public override string ProceedToQuest(MonsterType monsterType)
    {
        string MonsterName = "";

        if (isProgress)
        {
            if (lastQuest < maxQuest)
            {
                if (lastQuest % 2 != 1)
                {
                    if (monsterType == MonsterType.Rock)
                    {
                        MonsterName = "돌";
                        disposalMonster++;
                    }
                }
                else
                {
                    if (monsterType == MonsterType.Iron)
                    {
                        MonsterName = "철광";
                        disposalMonster++;
                    }
                }

                Debug.Log(MonsterName + " 정령 한마리 처치! 남은 몬스터는 " + (assignmentMonster - disposalMonster) + "마리");
            }
            CompleteToQuest();
        }
        return questStateType.ToString();
    }
}