using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageUIPresenter : MonoBehaviour
{
    [SerializeField] private Text aVillageQuestContentsText;
    [SerializeField] private Text bVillageQuestContentsText;
    [SerializeField] private Text aQuestAcceptanceButtonText;
    [SerializeField] private Text bQuestAcceptanceButtonText;
    [SerializeField] private GameObject questViewPanel;
    [SerializeField] private GameObject WorldMapPanel;
    [SerializeField] private Button questExitButton;

    private HuntingQuest aVillageQuest;
    private HuntingQuest bVillageQuest;

    private void Awake()
    {
        aVillageQuest = new AVillageQuest();
        bVillageQuest = new BVillageQuest();
        aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest();
        bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest();
    }

    public void OnClickAcceptButton(int villageType)
    {
        if (villageType == 0 && aQuestAcceptanceButtonText.text == "완료")
        {
            aVillageQuestContentsText.text = aVillageQuest.ShowContentsOfQuest();
        }
        else if (villageType == 1 && bQuestAcceptanceButtonText.text == "완료")
        {
            bVillageQuestContentsText.text = bVillageQuest.ShowContentsOfQuest();
        }

        if (villageType == 0)
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.AcceptToQuest();
        }

        else if (villageType == 1)
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.AcceptToQuest();
        }
    }

    public void OnClickKillAMonster(int monsterType)
    {
        if (monsterType == 0 || monsterType == 1)
        {
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest((MonsterType)monsterType);
        }

        else
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest((MonsterType)monsterType);
        }
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
            aQuestAcceptanceButtonText.text = aVillageQuest.ProceedToQuest(MonsterType.Wheat);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(MonsterType.Rock);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            bQuestAcceptanceButtonText.text = bVillageQuest.ProceedToQuest(MonsterType.Iron);
        }
    }
}
