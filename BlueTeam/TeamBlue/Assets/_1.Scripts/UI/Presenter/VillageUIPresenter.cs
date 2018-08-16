using UnityEngine;
using UnityEngine.UI;
using ProjectB.Quest;
using ProjectB.GameManager;
using System.Collections.Generic;

public class VillageUIPresenter : MonoBehaviour
{
    [SerializeField] Text aVillageQuestContentsText;
    [SerializeField] Text bVillageQuestContentsText;
    [SerializeField] Text aQuestAcceptanceButtonText;
    [SerializeField] Text bQuestAcceptanceButtonText;
    [SerializeField] Text questSubViewAVillageQuestContentsText;
    [SerializeField] Text questSubViewBAvillageQuestContentsText;
    [SerializeField] GameObject questViewPanel;
    [SerializeField] GameObject worldMapPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject combinationStorePanel;
    [SerializeField] Button questExitButton;

    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] List<Slot> slots = new List<Slot>();
    [SerializeField] List<Slot> combinationSlots = new List<Slot>();
    [SerializeField] List<Item> combinationItems = new List<Item>();
    [SerializeField] List<Item> combinationResourcesItems = new List<Item>();

    int[] requirematerials;

    IQuestViable aVillageQuest;
    IQuestViable bVillageQuest;
    string inventoryRelatedName;
    public string lastPress;

    private void Awake()
    {
        requirematerials = new int[4];
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

    public void OnClickInventoryRelatedButton(string name)
    {
        if (name == "Inventory")
        {

        }

        else if (name == "Combination")
        {
            combinationStorePanel.SetActive(true);
        }

        else if (name == "Trade")
        {
            // 트레이드 창을 닫는다.
        }

        inventoryRelatedName = name;
        inventoryPanel.SetActive(true);
    }

    public void OnClickInventoryRelatedExitButton()
    {
        if (inventoryRelatedName == "Combination")
        {
            combinationStorePanel.SetActive(false);
        }

        else if (inventoryRelatedName == "Trade")
        {
            // 트레이드 창을 닫는다.
        }

        inventoryPanel.SetActive(false);
        inventoryRelatedName = "";
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

    public void AddItem(int code)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Code == code)
            {
                if (items[i].ItemType != ItemType.Equipmentable)
                {
                    Debug.Log("갯수 증가");
                }
                else
                {
                    continue;
                }
                break;
            }

            else if (items[i].Code == 0)
            {
                items[i].SetItem(code);
                items[i].Text_Test.text = items[i].ItemName;
                break;
            }
        }
    }

    public void SwapOnClick(Slot slot)
    {
        int SlotIndex;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slot.IsClicked && slots[i].IsClicked)
            {
                if (slot == slots[i])
                {
                    continue;
                }
                SlotIndex = slot.transform.GetSiblingIndex();
                slot.transform.SetSiblingIndex(slots[i].transform.GetSiblingIndex());
                slots[i].transform.SetSiblingIndex(SlotIndex);
                slot.IsClicked = false;
                slots[i].IsClicked = false;
                break;
            }
            else
            {
                continue;
            }
        }
    }

    public void SwapOnCombination(Slot slot)
    {
        if(lastPress == "InventorySlot")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slot.IsClicked && slots[i].IsClicked)
                {
                    if (slot == slots[i])
                    {
                        continue;
                    }

                    if (slots[i].GetComponent<Item>().ItemType == ItemType.Exapandable)
                    {
                        if (slots[i].GetComponent<Item>().Code % 100 > 10)
                        {
                            slot.GetComponent<Item>().SwapItem(slots[i].GetComponent<Item>());
                            slot.GetComponent<Item>().Text_Test.text = slot.GetComponent<Item>().ItemName;
                            slots[i].GetComponent<Item>().Text_Test.text = slots[i].GetComponent<Item>().ItemName;
                            DisplayToResourcesSlot();
                            slot.IsClicked = false;
                            slots[i].IsClicked = false;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        if (lastPress == "CombinationSlot")
        {
            for (int i = 0; i < combinationSlots.Count; i++)
            {
                if(slot.IsClicked && combinationSlots[i].IsClicked)
                {
                    slot.gameObject.GetComponent<Item>().SwapItem(combinationSlots[i].gameObject.GetComponent<Item>());
                    slot.GetComponent<Item>().Text_Test.text = slot.GetComponent<Item>().ItemName;
                    combinationSlots[i].GetComponent<Item>().Text_Test.text = combinationSlots[i].GetComponent<Item>().ItemName;
                    DisplayToResourcesSlot();
                    slot.IsClicked = false;
                    combinationSlots[i].IsClicked = false;
                }
                break;
            }
        }
    }

    public void DisplayToResourcesSlot()
    {
        combinationResourcesItems[0].Text_Test.text = combinationItems[0].RecipeBrick.ToString();
        combinationResourcesItems[1].Text_Test.text = combinationItems[0].RecipeWood.ToString();
        combinationResourcesItems[2].Text_Test.text = combinationItems[0].RecipeIron.ToString();
        combinationResourcesItems[3].Text_Test.text = combinationItems[0].RecipeSheep.ToString();

        requirematerials[0] = combinationItems[0].RecipeWood;
        requirematerials[1] = combinationItems[0].RecipeIron;
        requirematerials[2] = combinationItems[0].RecipeBrick;
        requirematerials[3] = combinationItems[0].RecipeSheep;
    }

    public void OnClickCombinationItemButton()
    {
        bool isCombination = false;
        for(int i=0;i<4;i++)
        {
            if (GameDataManager.Instance.PlayerGamedata[3000 + i] < requirematerials[i])
                isCombination = false;
        }

        if(isCombination)
        {
            AddItem(combinationItems[0].Code);
            Debug.Log("조합성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }
}
