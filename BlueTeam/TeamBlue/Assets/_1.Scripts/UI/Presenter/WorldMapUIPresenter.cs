using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

public class WorldMapUIPresenter : MonoBehaviour
{
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
        GameControllManager.Instance.MoveNextScene(LoadType.Village, dungeonNumber);
        Debug.Log("마을 입장");
    }
    public void OnClickBossButton()
    {
        GameControllManager.Instance.MoveNextScene(LoadType.BossDungeon, 1);
    }

}
