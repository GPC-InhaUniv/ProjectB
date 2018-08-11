using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Iron : MonoBehaviour, IResource
{
    const int SheepWeightedValue = 5;

    const int BrickWeightedValue = 4;

    const int WoodWeightedValue = 3;

    const int IronWeightedValue = 10;

    public bool CheckTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        if (tradeProbability >= Random.Range(1, 100))
        {
            switch (resourceType)
            {
                case GameResources.Brick:
                    tradeProbability -= (int)((BrickWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Iron:
                    tradeProbability += (int)((IronWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Sheep:
                    tradeProbability -= (int)((SheepWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Wood:
                    tradeProbability -= (int)((WoodWeightedValue * sendingResourceCount) * 0.95);

                    break;
            }

            return true;
        }

        else
        {
            return false;
        }
    }

    public void ReceiveResources(int receivingResourceCount)
    {
        GameDataManager.Instance.PlayerGamedata[3001] += receivingResourceCount;
    }

    public void SendResources(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        if (GameDataManager.Instance.PlayerGamedata[3001] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3001] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 철광석 부족");
        }
    }
}
