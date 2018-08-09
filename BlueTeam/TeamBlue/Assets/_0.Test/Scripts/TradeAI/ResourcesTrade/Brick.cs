using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Brick : MonoBehaviour, IResource
{
    int SheepWeightedValue = 0;

    int BrickWeightedValue = 3;

    int WoodWeightedValue = 2;

    int IronWeightedValue = 1;

    public void SendResources(int sendingResourceCount)
    {
        if(GameDataManager.Instance.PlayerGamedata[3002] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3002] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 흙 부족");
        }

    }

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        GameDataManager.Instance.PlayerGamedata[3002] += receivingResourceCount;

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
                tradeProbability -= (int)((WoodWeightedValue * receivingResourceCount) * 0.95);

                break;
        }
    }
}
