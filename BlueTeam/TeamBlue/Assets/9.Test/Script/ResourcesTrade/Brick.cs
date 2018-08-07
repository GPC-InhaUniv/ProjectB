using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Brick : MonoBehaviour, IResource
{
    int SheepWeightedValue = 0;

    int BrickWeightedValue = 3;

    int WoodWeightedValue = 2;

    int IronWeightedValue = 1;

    public void SendResources(int sendingResourceCount)
    {
        if(GameData.Instance.PlayerGamedata[3002] >= sendingResourceCount)
        {
            GameData.Instance.PlayerGamedata[3002] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 흙 부족");
        }

    }

    public void ReceiveResources(int receivingResourceCount, GameResources resourceType, ref int relationShip)
    {
        switch (resourceType)
        {
            case GameResources.Brick:
                GameData.Instance.PlayerGamedata[3002] += (receivingResourceCount + BrickWeightedValue);
                relationShip -= (int)(relationShip + ((BrickWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Iron:
                GameData.Instance.PlayerGamedata[3002] += (receivingResourceCount + IronWeightedValue);
                relationShip -= (int)(relationShip + ((IronWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Sheep:
                GameData.Instance.PlayerGamedata[3002] += (receivingResourceCount + SheepWeightedValue);
                relationShip -= (int)(relationShip + ((SheepWeightedValue * receivingResourceCount)) * 0.95);

                break;

            case GameResources.Wood:
                GameData.Instance.PlayerGamedata[3002] += (receivingResourceCount + WoodWeightedValue);
                relationShip -= (int)(relationShip + ((WoodWeightedValue * receivingResourceCount)) * 0.95);

                break;
        }
    }
}
