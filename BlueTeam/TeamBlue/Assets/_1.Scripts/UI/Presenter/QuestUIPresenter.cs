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

    IQuestViable aVillageQuest;
    IQuestViable bVillageQuest;

    private void Awake()
    {
        aVillageQuest = new AVillageHuntingQuest();
        bVillageQuest = new BVillageHuntingQuest();
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest(QuestType.AVillageQuest, "완료");
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest(QuestType.BVillageQuest, "완료");
    }

    public void OnClickAcceptButton(int villageType)
    {
<<<<<<< HEAD
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest((QuestType)villageType, bQuestAcceptanceButtonText.text);
=======
         aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);
         bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest((QuestType)villageType, bQuestAcceptanceButtonText.text);
>>>>>>> 577de2a63bd2fc72aae4d678cbe548b7e93868e7

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
