using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Iron : IResource
{
    const int SheepWeightedValue = 5;

    const int BrickWeightedValue = 4;

    const int WoodWeightedValue = 3;

    const int IronWeightedValue = 10;


    public int CalculateTradeProbability(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability += (int)((IronWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability -= (int)((WoodWeightedValue * receivingResourceCount) * 0.95);

                break;
        }


        if (tradeProbability > 100)
        {
            tradeProbability = 100;
        }

        return tradeProbability;

    }

    public void ReceiveResources(int receivingResourceCount)
    {
        // GameDataManager.Instance.PlayerGamedata[3001] += receivingResourceCount;
        TestResource.Instance.testDictionary["Iron"] += receivingResourceCount;

    }

    public void SendResources(int sendingResourceCount)
    {
        //if (GameDataManager.Instance.PlayerGamedata[3001] >= sendingResourceCount)
        //{
        //    GameDataManager.Instance.PlayerGamedata[3001] -= sendingResourceCount;
        //}

        if(TestResource.Instance.testDictionary["Iron"] >= sendingResourceCount)
        {
            TestResource.Instance.testDictionary["Iron"] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 철광석 부족");
        }
    }
}
