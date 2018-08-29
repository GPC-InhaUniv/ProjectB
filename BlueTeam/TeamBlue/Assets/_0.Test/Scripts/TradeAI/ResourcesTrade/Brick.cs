using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Brick : IResource
{
    const int SheepWeightedValue = 3;

    const int BrickWeightedValue = 10;

    const int WoodWeightedValue = 5;

    const int IronWeightedValue = 4;


    public int CalculateTradeProbability(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability += (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability -= (int)((IronWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability -= (int)((WoodWeightedValue * receivingResourceCount) * 0.95);

                break;
        }

        if(tradeProbability > 100)
        {
            tradeProbability = 100;
        }

        return tradeProbability;
    }

    public void ReceiveResources(int receivingResourceCount)
    {
        GameDataManager.Instance.PlayerGamedata[3002] += receivingResourceCount;

        //TestResource.Instance.testDictionary["Brick"] += receivingResourceCount;
    }

    public void SendResources(int sendingResourceCount)
    {
        if (GameDataManager.Instance.PlayerGamedata[3002] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3002] -= sendingResourceCount;
        }

        /*if(TestResource.Instance.testDictionary["Brick"] >= sendingResourceCount)
        {
            TestResource.Instance.testDictionary["Brick"] -= sendingResourceCount;
        }*/

        else
        {
            Debug.Log("보낼 흙 부족");
        }
    }
}
