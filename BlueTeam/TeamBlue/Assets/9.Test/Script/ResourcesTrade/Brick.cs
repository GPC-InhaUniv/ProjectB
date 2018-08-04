using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Brick : MonoBehaviour, IResource
{
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

    public void ReceiveResources(int receivingResourceCount)
    {
        GameData.Instance.PlayerGamedata[3002] += receivingResourceCount;
    }
}
