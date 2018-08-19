using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Iron : MonoBehaviour, IResource
{
    const int SheepWeightedValue = 5;

    const int BrickWeightedValue = 4;

    const int WoodWeightedValue = 3;

    const int IronWeightedValue = 10;

    public void CalculateTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability -= (int)((BrickWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability += (int)((IronWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability -= (int)((WoodWeightedValue * sendingResourceCount) * 0.95);

                break;
        }


        if (tradeProbability > 100)
        {
            tradeProbability = 100;
        }

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
