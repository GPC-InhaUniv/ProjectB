using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                GameDataManager.Instance.PlayerGamedata[3003] += (receivingResourceCount + BrickWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((BrickWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Iron:
                GameDataManager.Instance.PlayerGamedata[3003] += (receivingResourceCount + IronWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((IronWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Sheep:
                GameDataManager.Instance.PlayerGamedata[3003] += (receivingResourceCount + SheepWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((SheepWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Wood:
                GameDataManager.Instance.PlayerGamedata[3003] += (receivingResourceCount + WoodWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((WoodWeightedValue * receivingResourceCount)) * 0.95);

                break;
        }
    }
}
