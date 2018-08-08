using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Quest;

public class VillageUIPresenter : MonoBehaviour
{
    [SerializeField] private Text aVillageQuestContentsText;
    [SerializeField] private Text bVillageQuestContentsText;
    [SerializeField] private Text aQuestAcceptanceButtonText;
    [SerializeField] private Text bQuestAcceptanceButtonText;
    [SerializeField] private GameObject questViewPanel;
    [SerializeField] private GameObject WorldMapPanel;
    [SerializeField] private Button questExitButton;

    private IQuestViable aVillageQuest;
    private IQuestViable bVillageQuest;

    private void Awake()
    {
        aVillageQuest = new AVillageHuntingQuest();
        bVillageQuest = new BVillageHuntingQuest();
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest(QuestType.AVillageQuest, );
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest();
    }

    public void OnClickAcceptButton(int villageType)
    {
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);

        aQuestAcceptanceButtonText.text = aVillageQuest.AcceptToQuest((QuestType)villageType);
        bQuestAcceptanceButtonText.text = bVillageQuest.AcceptToQuest((QuestType)villageType);
    }

    public void OnClickKillAMonster(int monsterType)
    {
        aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest((ConditionType)monsterType);
        bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest((ConditionType)monsterType);
    }

    public void OnClickQuestButton()
    {
        questViewPanel.SetActive(true);
    }

    public void OnClickQuestExitButton()
    {
        questViewPanel.SetActive(false);
    }

    public void OnClickWoodDungeonButton(int dungeonNumber)
    {
        LoadingSceneManager.Instance.LoadScene(LoadType.WoodDungeon, dungeonNumber);
        Debug.Log("나무 던전 입장");
    }

    public void OnClickIronDungeonButton(int dungeonNumber)
    {
        LoadingSceneManager.Instance.LoadScene(LoadType.IronDungeon, dungeonNumber);
        Debug.Log("철광석 던전 입장");
    }

    public void OnClickBrickDungeonButton(int dungeonNumber)
    {
        LoadingSceneManager.Instance.LoadScene(LoadType.BrickDungeon, dungeonNumber);
        Debug.Log("돌 던전 입장");
    }

    public void OnClickSheepDungeonButton(int dungeonNumber)
    {
        LoadingSceneManager.Instance.LoadScene(LoadType.SheepDungeon, dungeonNumber);
        Debug.Log("양 던전 입장");
    }

    public void OnClickVillageButton(int dungeonNumber)
    {
        LoadingSceneManager.Instance.LoadScene(LoadType.Village, dungeonNumber);
        Debug.Log("마을 입장");
    }

    public void OnClickEntranceDungeonButton()
    {
        WorldMapPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest(MonsterType.Wood);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest(MonsterType.Sheep);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(MonsterType.Brick);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(MonsterType.Iron);
        }
    }
}
