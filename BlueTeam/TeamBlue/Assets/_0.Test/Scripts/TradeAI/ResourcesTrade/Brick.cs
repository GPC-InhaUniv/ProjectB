using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

// bool형 메서드로 거래 확률 계산

class Brick : MonoBehaviour, IResource
{
    const int SheepWeightedValue = 3;

    const int BrickWeightedValue = 10;

    const int WoodWeightedValue = 5;

    const int IronWeightedValue = 4;


    public bool CheckTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        if (tradeProbability >= Random.Range(1, 100))
        {
            switch (resourceType)
            {
                case GameResources.Brick:
                    tradeProbability += (int)((BrickWeightedValue * sendingResourceCount) * 0.95);

                    break;

                case GameResources.Iron:
                    tradeProbability -= (int)((IronWeightedValue * sendingResourceCount) * 0.95);

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
        GameDataManager.Instance.PlayerGamedata[3002] += receivingResourceCount;

    }

    public void SendResources(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        if (GameDataManager.Instance.PlayerGamedata[3002] >= sendingResourceCount)
        {
            GameDataManager.Instance.PlayerGamedata[3002] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 흙 부족");
        }
    }
}
