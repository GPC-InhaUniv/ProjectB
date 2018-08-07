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
        if (GameData.Instance.PlayerGamedata[3003] >= sendingResourceCount)
        {
            GameData.Instance.PlayerGamedata[3003] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보내는 양 부족");
        }

    }

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, ref int relationShip)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                GameData.Instance.PlayerGamedata[3003] += (receivingResourceCount + BrickWeightedValue);
                relationShip -= (int)(relationShip + ((BrickWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Iron:
                GameData.Instance.PlayerGamedata[3003] += (receivingResourceCount + IronWeightedValue);
                relationShip -= (int)(relationShip + ((IronWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Sheep:
                GameData.Instance.PlayerGamedata[3003] += (receivingResourceCount + SheepWeightedValue);
                relationShip -= (int)(relationShip + ((SheepWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Wood:
                GameData.Instance.PlayerGamedata[3003] += (receivingResourceCount + WoodWeightedValue);
                relationShip -= (int)(relationShip + ((WoodWeightedValue * receivingResourceCount)) * 0.95);

                break;
        }
    }
}
