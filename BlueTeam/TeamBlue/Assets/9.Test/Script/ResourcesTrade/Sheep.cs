using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Sheep : MonoBehaviour, IResource
{
    int SheepWeightedValue = 5;

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

    public void ReceiveResources(int receivingResourceCount)
    {
        GameData.Instance.PlayerGamedata[3003] += receivingResourceCount;
    }
}
