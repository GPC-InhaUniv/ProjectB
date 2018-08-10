using UnityEngine;
using UnityEngine.UI;
using ProjectB.Quest;
using ProjectB.GameManager;

public class VillageUIPresenter : MonoBehaviour
{
    [SerializeField] private Text aVillageQuestContentsText;
    [SerializeField] private Text bVillageQuestContentsText;
    [SerializeField] private Text aQuestAcceptanceButtonText;
    [SerializeField] private Text bQuestAcceptanceButtonText;
    [SerializeField] private Text questSubViewAVillageQuestContentsText;
    [SerializeField] private Text questSubViewBAvillageQuestContentsText;
    [SerializeField] private GameObject questViewPanel;
    [SerializeField] private GameObject worldMapPanel;
    [SerializeField] private Button questExitButton;

    private IQuestViable aVillageQuest;
    private IQuestViable bVillageQuest;

    private void Awake()
    {
        aVillageQuest = new AVillageHuntingQuest();
        bVillageQuest = new BVillageHuntingQuest();
/*        questSubViewAVillageQuestContentsText.text = */aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest(QuestType.AVillageQuest, "완료");
/*        questSubViewBAvillageQuestContentsText.text = */bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest(QuestType.BVillageQuest, "완료");
    }

    public void OnClickAcceptButton(int villageType)
    {
        questSubViewAVillageQuestContentsText.text = aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest((QuestType)villageType, aQuestAcceptanceButtonText.text);
        questSubViewBAvillageQuestContentsText.text = bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest((QuestType)villageType, bQuestAcceptanceButtonText.text);

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

    public void OnClickEntranceDungeonButton()
    {
        worldMapPanel.SetActive(true);
    }

    public void OnClickWoodDungeonButton(int dungeonNumber)
    {
        GameControllManager.Instance.MoveNextScene(LoadType.WoodDungeon, dungeonNumber);
        Debug.Log("나무 던전 입장");
    }

    public void OnClickIronDungeonButton(int dungeonNumber)
    {
        GameControllManager.Instance.MoveNextScene(LoadType.IronDungeon, dungeonNumber);
        Debug.Log("철광석 던전 입장");
    }

    public void OnClickBrickDungeonButton(int dungeonNumber)
    {
        GameControllManager.Instance.MoveNextScene(LoadType.BrickDungeon, dungeonNumber);
        Debug.Log("돌 던전 입장");
    }

    public void OnClickSheepDungeonButton(int dungeonNumber)
    {
        GameControllManager.Instance.MoveNextScene(LoadType.SheepDungeon, dungeonNumber);
        Debug.Log("양 던전 입장");
    }

    public void OnClickVillageButton(int dungeonNumber)
    {
        LoadingSceneManager.LoadScene(LoadType.Village, dungeonNumber);
        Debug.Log("마을 입장");
    }

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
