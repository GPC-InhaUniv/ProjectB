using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterType
{
    Wood,
    Wheat,
    Rock,
    Iron,
}

public enum QuestType
{
    AVillageQuest,
    BVillageQuest,
}

public enum QuestStateType
{
    수락, // 수락, Acceptance
    진행, // 진행, Progress
    완료, // 완료, Completion
}

interface IQuestViable
{
    //퀘스트를 수락한다.
    string AcceptToQuest();
    //퀘스트 내용을 보여주다.
    string ShowContentsOfQuest();
    //퀘스트를 진행한다.
    string ProceedToQuest(MonsterType monsterType);
    //퀘스트를 완료한다.
    void CompleteToQuest();
}

abstract public class HuntingQuest : IQuestViable
{
    protected const int defaultMonsterCount = 30;   // 기본 몬스터 마리 수
    protected const int maxQuest = 10;              // Quest 진행 최대 수
    protected int additionMonsterCount;             // 퀘스트 클리어시 추가되는 몬스터 마리 수
    protected int lastQuest;                        // DataManager에서 LastQuest를 받아야하는 데이터
    protected int assignmentMonster;                // 잡아야할 몬스터
    protected int disposalMonster;                  // 잡은 몬스터
    protected bool isProgress;                      // 퀘스트를 수락했는지 안했는지
    protected QuestStateType questStateType;        // 퀘스트 진행 상태

    abstract public string AcceptToQuest();                         //퀘스트를 수락한다.
    abstract public string ShowContentsOfQuest();                   //퀘스트 내용을 보여주다.
    abstract public string ProceedToQuest(MonsterType monsterType); //퀘스트를 진행한다.
    //퀘스트를 완료한다.
    public void CompleteToQuest()
    {
        if (assignmentMonster == disposalMonster)
        {
            isProgress = false;
            questStateType = QuestStateType.완료;
            additionMonsterCount += 10;
            disposalMonster = 0;
            assignmentMonster = 0;
            lastQuest++;
        }
    }
}

















































//    public void KillAMonster(MonsterType monsterType)
//    {
//        string MonsterName = "";

//        if (isProgress && ALastQuest < 10)
//        {
//            switch (monsterType)
//            {
//                case MonsterType.Wood:
//                    aDisposalMonster++;
//                    MonsterName = "나무";
//                    break;
//                case MonsterType.Wheat:
//                    aDisposalMonster++;
//                    MonsterName = "밀";
//                    break;
//                case MonsterType.Rock:
//                    bDisposalMonster++;
//                    MonsterName = "돌";
//                    break;
//                case MonsterType.Iron:
//                    bDisposalMonster++;
//                    MonsterName = "철";
//                    break;
//            }
//            if(monsterType == MonsterType.Wood || monsterType == MonsterType.Wheat)
//            {
//                Debug.Log(MonsterName + " 정령 한마리 처치! 남은 몬스터는 " + (aAssignmentMonster - aDisposalMonster) + "마리 입니다.");
//            }

//            else
//            {
//                Debug.Log(MonsterName + " 정령 한마리 처치! 남은 몬스터는 " + (bAssignmentMonster - bDisposalMonster) + "마리 입니다.");
//            }
//        }
//    }

//    // A마을 퀘스트
//    public void OnClickTree(Text buttonText)
//    {
//        aDisposalMonster++;

//        if (isProgress != false)
//        {
//            if (ALastQuest % 2 != 0 && ALastQuest < 10)
//            {
//                if (aDisposalMonster != aAssignmentMonster)
//                {
//                    Debug.Log("나무 정령 한마리 처치! 남은 몬스터는 " + (aAssignmentMonster - aDisposalMonster) + "마리 입니다.");
//                }

//                else
//                {
//                    Debug.Log("퀘스트 완료! 보상을 수령하십시오.");
//                    aDisposalMonster = 0;
//                    ALastQuest++;
//                    buttonText.text = "완료";
//                    isProgress = false;
//                }
//            }
//        }
//    }

//    // A마을 퀘스트
//    public void OnClickWheat(Text buttonText)
//    {
//        aDisposalMonster++;

//        if (isProgress != false)
//        {
//            if (ALastQuest % 2 != 1 && ALastQuest < 10)
//            {
//                if (aDisposalMonster != aAssignmentMonster)
//                {
//                    Debug.Log("밀 정령 한마리 처치! 남은 몬스터는 " + (aAssignmentMonster - aDisposalMonster) + "마리 입니다.");
//                }

//                else
//                {
//                    Debug.Log("퀘스트 완료! 보상을 수령하십시오.");
//                    aDisposalMonster = 0;
//                    ALastQuest++;
//                    buttonText.text = "완료";
//                    isProgress = false;
//                }
//            }
//        }
//    }

//    // B마을 퀘스트
//    public void OnClickRock(Text buttonText)
//    {
//        bDisposalMonster++;

//        if (isProgress != false)
//        {
//            if (BLastQuest % 2 != 0 && BLastQuest < 10)
//            {
//                if (bDisposalMonster != bAssignmentMonster)
//                {
//                    Debug.Log("돌 정령 한마리 처치! 남은 몬스터는 " + (bAssignmentMonster - bDisposalMonster) + "마리 입니다.");
//                }

//                else
//                {
//                    Debug.Log("퀘스트 완료! 보상을 수령하십시오.");
//                    bDisposalMonster = 0;
//                    BLastQuest++;
//                    buttonText.text = "완료";
//                    isProgress = false;
//                }
//            }
//        }
//    }

//    public void OnClickIron(Text buttonText)
//    {
//        bDisposalMonster++;

//        if (isProgress != false)
//        {
//            if (BLastQuest % 2 != 1 && BLastQuest < 10)
//            {
//                if (bDisposalMonster != bAssignmentMonster)
//                {
//                    Debug.Log("철광석 정령 한마리 처치! 남은 몬스터는 " + (bAssignmentMonster - bDisposalMonster) + "마리 입니다.");
//                }

//                else
//                {
//                    Debug.Log("퀘스트 완료! 보상을 수령하십시오.");
//                    bDisposalMonster = 0;
//                    BLastQuest++;
//                    buttonText.text = "완료";
//                    isProgress = false;
//                }
//            }
//        }
//    }

//    public void OnClickAVilligeAcceptance(Text buttonText, Text text)
//    {
//        if (buttonText.text == "수락")
//        {
//            buttonText.text = "진행중";
//            isProgress = true;
//        }

//        else if (buttonText.text == "완료")
//        {
//            Debug.Log("경험치 500, 발전카드 보상");
//            playerLevel += 2;
//            buttonText.text = "수락";

//            SetAVilligeQuestContent(text);
//        }
//    }

//    public void OnClickBVilligeAcceptance(Text buttonText, Text text)
//    {
//        if (buttonText.text == "수락")
//        {
//            buttonText.text = "진행중";
//            isProgress = true;
//        }

//        else if (buttonText.text == "완료")
//        {
//            Debug.Log("경험치 500, 발전카드 보상");
//            playerLevel += 2;
//            buttonText.text = "수락";

//            SetBVilligeQuestcontent(text);
//        }
//    }


//    public void SetAVilligeQuestContent(Text text)
//    {
//        aAssignmentMonster = playerLevel * 5;

//        if (ALastQuest % 2 != 0 && ALastQuest < 10)
//        {
//            text.text = "A마을 퀘스트\n나무 몬스터 " + aAssignmentMonster + "마리 사냥";
//        }
//        else if (ALastQuest % 2 != 1 && ALastQuest < 10)
//        {
//            text.text = "A마을 퀘스트\n밀 몬스터 " + aAssignmentMonster + "마리 사냥";
//        }
//        else
//        {
//            text.text = "A마을 퀘스트\n퀘스트 모두 완료";
//        }
//    }

//    public void SetBVilligeQuestcontent(Text text)
//    {
//        bAssignmentMonster = playerLevel * 5;

//        if (BLastQuest % 2 != 0 && BLastQuest < 10)
//        {
//            text.text = "B마을 퀘스트\n돌 몬스터 " + bAssignmentMonster + "마리 사냥";
//        }
//        else if (BLastQuest % 2 != 1 && BLastQuest < 10)
//        {
//            text.text = "B마을 퀘스트\n철광 몬스터 " + bAssignmentMonster + "마리 사냥";
//        }
//        else
//        {
//            text.text = "B마을 퀘스트\n퀘스트 모두 완료";
//        }
//    }
//}