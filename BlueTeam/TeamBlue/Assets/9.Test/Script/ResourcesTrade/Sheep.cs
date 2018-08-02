using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct TestItem
{
    public int itemBrick;
    public int itemWood;
    public int itemSheep;
    public int itemIron;
}

class Sheep : MonoBehaviour, IResource
{
    int SheepWeightedValue = 5;
    int BrickWeightedValue = 1;
    int WoodWeightedValue = 2;
    int IronWeightedValue = 0;

    TestItem testItem = new TestItem();

    GameResources gameResources;

    public void TradeResources()
    {
        switch(gameResources)
        {
            case GameResources.Brick:
                break;

            case GameResources.Iron:
                break;
                
            case GameResources.Sheep:
                break;

            case GameResources.Wood:
                break;
        }
    }
}
