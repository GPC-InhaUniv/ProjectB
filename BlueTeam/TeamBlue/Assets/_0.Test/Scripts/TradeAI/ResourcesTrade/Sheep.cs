using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectB.GameManager;

class Sheep : MonoBehaviour, IResource
{
    int SheepWeightedValue = 3;

    int BrickWeightedValue = 1;

    int WoodWeightedValue = 2;

    int IronWeightedValue = 0;

    public void SendResources(int sendingResourceCount)
    {
        if (GameDataManager.Instance.PlayerGamedata[3003] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3003] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보내는 양 부족");
        }

    }

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        GameDataManager.Instance.PlayerGamedata[3003] += receivingResourceCount;

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
