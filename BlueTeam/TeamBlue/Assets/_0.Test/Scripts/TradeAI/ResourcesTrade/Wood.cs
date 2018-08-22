﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Wood : IResource
{
    const int SheepWeightedValue = 4;

    const int BrickWeightedValue = 5;

    const int WoodWeightedValue = 10;

    const int IronWeightedValue = 3;

    public int CalculateTradeProbability(int sendingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability -= (int)((BrickWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability -= (int)((IronWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * sendingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability += (int)((WoodWeightedValue * sendingResourceCount) * 0.95);

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
        //GameDataManager.Instance.PlayerGamedata[3000] += receivingResourceCount;
        TestResource.Instance.testDictionary["Wood"] += receivingResourceCount;
    }

    public void SendResources(int sendingResourceCount)
    {
        //if (GameDataManager.Instance.PlayerGamedata[3000] >= sendingResourceCount)
        //{
        //    GameDataManager.Instance.PlayerGamedata[3000] -= sendingResourceCount;

        //}

        if(TestResource.Instance.testDictionary["Wood"] >= sendingResourceCount)
        {
            TestResource.Instance.testDictionary["Wood"] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보내는 나무 부족");
        }
    }
}
