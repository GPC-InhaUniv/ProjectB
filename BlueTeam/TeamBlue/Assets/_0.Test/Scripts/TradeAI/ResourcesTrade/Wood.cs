using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class Wood : MonoBehaviour, IResource
{
    const int SheepWeightedValue = 4;

    const int BrickWeightedValue = 5;

    const int WoodWeightedValue = 10;

    const int IronWeightedValue = 3;

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
                    tradeProbability -= (int)((IronWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Sheep:
                    tradeProbability -= (int)((SheepWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Wood:
                    tradeProbability += (int)((WoodWeightedValue * sendingResourceCount) * 0.95);

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
        GameDataManager.Instance.PlayerGamedata[3000] += receivingResourceCount;
    }

    public void SendResources(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
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
