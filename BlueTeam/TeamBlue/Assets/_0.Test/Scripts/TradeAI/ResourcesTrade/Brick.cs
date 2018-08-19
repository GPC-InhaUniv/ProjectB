using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Brick : MonoBehaviour, IResource
{
    const int SheepWeightedValue = 3;

    const int BrickWeightedValue = 10;

    const int WoodWeightedValue = 5;

    const int IronWeightedValue = 4;


    public void CalculateTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability += (int)((BrickWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability -= (int)((IronWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability -= (int)((WoodWeightedValue * sendingResourceCount) * 0.95);

                break;
        }

        if(tradeProbability > 100)
        {
            tradeProbability = 100;
        }
    }

    public void ReceiveResources(int receivingResourceCount)
    {
        //GameDataManager.Instance.PlayerGamedata[3002] += receivingResourceCount;

        TestResource.Instance.testDictionary["Brick"] += receivingResourceCount;
    }

    public void SendResources(int sendingResourceCount)
    {
        //if (GameDataManager.Instance.PlayerGamedata[3002] >= sendingResourceCount)
        //{
        //    GameDataManager.Instance.PlayerGamedata[3002] -= sendingResourceCount;
        //}

        if(TestResource.Instance.testDictionary["Brick"] >= sendingResourceCount)
        {
            TestResource.Instance.testDictionary["Brick"] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 흙 부족");
        }
    }
}
