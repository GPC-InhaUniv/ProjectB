using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wood : MonoBehaviour, IResource
{
    int SheepWeightedValue = 0;

    int BrickWeightedValue = 2;

    int WoodWeightedValue = 3;

    int IronWeightedValue = 1;

    public void SendResources(int sendingResourceCount)
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

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                GameDataManager.Instance.PlayerGamedata[3000] += (receivingResourceCount + BrickWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((BrickWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Iron:
                GameDataManager.Instance.PlayerGamedata[3000] += (receivingResourceCount + IronWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((IronWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Sheep:
                GameDataManager.Instance.PlayerGamedata[3000] += (receivingResourceCount + SheepWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((SheepWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Wood:
                GameDataManager.Instance.PlayerGamedata[3000] += (receivingResourceCount + WoodWeightedValue);
                tradeProbability -= (int)(tradeProbability + ((WoodWeightedValue * receivingResourceCount)) * 0.95);

                break;
        }
    }
}
