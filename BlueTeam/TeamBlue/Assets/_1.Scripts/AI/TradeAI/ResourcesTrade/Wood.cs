using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Wood : IResource
{
    const int SheepWeightedValue = 4;

    const int BrickWeightedValue = 5;

    const int WoodWeightedValue = 10;

    const int IronWeightedValue = 3;


    public int CalculateTradeProbability(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                tradeProbability -= (int)((IronWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                tradeProbability -= (int)((SheepWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                tradeProbability += (int)((WoodWeightedValue * receivingResourceCount) * 0.95);

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
        GameDataManager.Instance.PlayerGamedata[3000] += receivingResourceCount;
    }

    public void SendResousrces(int sendingResourceCount)
    {
        if (GameDataManager.Instance.PlayerGamedata[3000] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3000] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보내는 나무 부족");
        }
    }
}
