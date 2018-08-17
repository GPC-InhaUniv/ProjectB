using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Quest;

public class QuestUIPresenter : MonoBehaviour
{
    [SerializeField] Text aVillageQuestContentsText;
    [SerializeField] Text bVillageQuestContentsText;
    [SerializeField] Text aQuestAcceptanceButtonText;
    [SerializeField] Text bQuestAcceptanceButtonText;
    [SerializeField] Text questSubViewAVillageQuestContentsText;
    [SerializeField] Text questSubViewBAvillageQuestContentsText;

    IQuestViable aVillageQuest;
    IQuestViable bVillageQuest;

    private void Awake()
    {
        aVillageQuest = new AVillageHuntingQuest();
        bVillageQuest = new BVillageHuntingQuest();
        /*        questSubViewAVillageQuestContentsText.text = */
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest(QuestType.AVillageQuest, "완료");
        /*        questSubViewBAvillageQuestContentsText.text = */
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest(QuestType.BVillageQuest, "완료");
    }

    public void OnClickAcceptButton(int villageType)
    {
        questSubViewAVillageQuestContentsText.text = aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);
        questSubViewBAvillageQuestContentsText.text = bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest((QuestType)villageType, bQuestAcceptanceButtonText.text);

        aQuestAcceptanceButtonText.text = aVillageQuest.AcceptToQuest((QuestType)villageType);
        bQuestAcceptanceButtonText.text = bVillageQuest.AcceptToQuest((QuestType)villageType);
    }

    // 삭제 예정
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest(ConditionType.WoodMonster);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest(ConditionType.SheepMonster);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(ConditionType.BrickMonster);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(ConditionType.IronMonster);
        }
    }
}
