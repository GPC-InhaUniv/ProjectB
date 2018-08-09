using UnityEngine;

namespace ProjectB.Quest
{
    class AVillageHuntingQuest : HuntingQuest
    {
        public AVillageHuntingQuest()
        {
            lastQuest = 0; // GameManager
            isProgress = false;
            questStateType = QuestStateType.수락;
        }

        public override string AcceptToQuest(QuestType questType)
        {
            if (lastQuest < maxQuest)
            {
                if (questType == QuestType.AVillageQuest)
                {
                    if (questStateType == QuestStateType.수락)
                    {
                        questStateType = QuestStateType.진행;
                        isProgress = true;
                    }
                    else if (questStateType == QuestStateType.완료)
                    {
                        questStateType = QuestStateType.수락;
                        Debug.Log("보상 : A마을 우호도 1, 경험치 500, 발전카드 1장");
                    }
                }
            }
            else
            {
                questStateType = QuestStateType.완료;
            }
            return questStateType.ToString();
        }

        public override string ShowContentsOfQuest(QuestType questType, string questState)
        {
            string MonsterName = "";
            assignmentMonster = defaultMonsterCount + additionMonsterCount;

            if (lastQuest < maxQuest)
            {
                if (questType == QuestType.AVillageQuest && questState == "완료")
                {
                    if (lastQuest % 2 != 1)
                    {
                        MonsterName = "나무";
                    }
                    else if (lastQuest % 2 != 0)
                    {
                        MonsterName = "밀";
                    }
                    QuestContents = "A마을 퀘스트\n" + MonsterName + " 몬스터를" + assignmentMonster + "마리 처치\n" + disposalMonster + "/" + assignmentMonster;
                }
            }
            else
            {
                QuestContents = "A마을 퀘스트\n퀘스트 모두 완료";
            }
            return QuestContents;
        }


        public override string ProceedToQuest(ConditionType conditionType)
        {
            string MonsterName = "";
            if (isProgress)
            {
                if (lastQuest < maxQuest)
                {
                    if (lastQuest % 2 != 1)
                    {
                        if (conditionType == ConditionType.WoodMonster)
                        {
                            MonsterName = "나무";
                            disposalMonster++;
                        }
                    }
                    else
                    {
                        if (conditionType == ConditionType.SheepMonster)
                        {
                            MonsterName = "밀";
                            disposalMonster++;
                        }
                    }
                    Debug.Log(MonsterName + "정령 한마리 처치! 남은 몬스터는 " + (assignmentMonster - disposalMonster) + "마리");
                }
                CompleteToQuest();
            }
            return questStateType.ToString();
        }
    }
}