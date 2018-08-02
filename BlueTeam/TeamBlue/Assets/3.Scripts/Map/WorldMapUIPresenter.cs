using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapUIPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject WorldMapPanel;

    public void OnClickWoodDungeonButton(int dungeonNumber)
    {
        LoadingScene.LoadScene(MapType.WoodDungeon, dungeonNumber);
        Debug.Log("나무 던전 입장");
    }

    public void OnClickIronDungeonButton(int dungeonNumber)
    {
        LoadingScene.LoadScene(MapType.IronDungeon, dungeonNumber);
        Debug.Log("철광석 던전 입장");
    }

    public void OnClickBrickDungeonButton(int dungeonNumber)
    {
        LoadingScene.LoadScene(MapType.BrickDungeon, dungeonNumber);
        Debug.Log("돌 던전 입장");
    }

    public void OnClickSheepDungeonButton(int dungeonNumber)
    {
        LoadingScene.LoadScene(MapType.SheepDungeon, dungeonNumber);
        Debug.Log("양 던전 입장");
    }

    public void OnClickVillageButton(int dungeonNumber)
    {
        LoadingScene.LoadScene(MapType.Village, dungeonNumber);
        Debug.Log("마을 입장");
    }

    public void OnClickEntranceDungeonButton()
    {
        WorldMapPanel.SetActive(true);
    }
}
