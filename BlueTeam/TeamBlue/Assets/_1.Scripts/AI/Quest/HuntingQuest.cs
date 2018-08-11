namespace ProjectB.Quest
{
    public enum ConditionType
    {
        WoodMonster,
        SheepMonster,
        BrickMonster,
        IronMonster,
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
        string AcceptToQuest(QuestType questType);                          //퀘스트를 수락한다.
        string ShowContentsOfQuest(QuestType questType, string questState); //퀘스트 내용을 보여주다.
        string ProceedToQuest(ConditionType conditionType);                 //퀘스트를 진행한다.
        void CompleteToQuest();                                             //퀘스트를 완료한다.
    }

    abstract public class HuntingQuest : IQuestViable
    {
        protected const int defaultMonsterCount = 30;   // 기본 몬스터 마리 수
        protected const int maxQuest = 10;              // Quest 진행 최대 수
        protected string QuestContents;                 // 현재 퀘스트 내용
        protected int additionMonsterCount;             // 퀘스트 클리어시 추가되는 몬스터 마리 수
        protected int lastQuest;                        // DataManager에서 LastQuest를 받아야하는 데이터
        protected int assignmentMonster;                // 잡아야할 몬스터
        protected int disposalMonster;                  // 잡은 몬스터
        protected bool isProgress;                      // 퀘스트를 수락했는지 안했는지
        protected QuestStateType questStateType;        // 퀘스트 진행 상태

        abstract public string AcceptToQuest(QuestType questType);                              //퀘스트를 수락한다.
        abstract public string ShowContentsOfQuest(QuestType questType, string questState);     //퀘스트 내용을 보여주다.
        abstract public string ProceedToQuest(ConditionType conditionType);                     //퀘스트를 진행한다.
                                                                                                // 퀘스트를 완료한다.
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
}