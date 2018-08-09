using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Iron : MonoBehaviour, IResource
{
    int SheepWeightedValue = 0;

    int BrickWeightedValue = 1;

    int WoodWeightedValue = 2;

    int IronWeightedValue = 3;

    public void SendResources(int sendingResourceCount)
    {
        if(GameDataManager.Instance.PlayerGamedata[3001] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3001] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 철광석 부족");
        }
    }

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                GameDataManager.Instance.PlayerGamedata[3002] += (receivingResourceCount + BrickWeightedValue);
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Iron:
                GameDataManager.Instance.PlayerGamedata[3001] += (receivingResourceCount + IronWeightedValue);
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Sheep:
                GameDataManager.Instance.PlayerGamedata[3003] += (receivingResourceCount + SheepWeightedValue);
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;

            case GameResources.Wood:
                GameDataManager.Instance.PlayerGamedata[3000] += (receivingResourceCount + WoodWeightedValue);
                tradeProbability -= (int)((BrickWeightedValue * receivingResourceCount) * 0.95);

                break;
        }
    }
}
