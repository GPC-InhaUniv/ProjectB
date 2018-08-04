using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Iron : MonoBehaviour, IResource
{

    public void SendResources(int sendingResourceCount)
    {
        if(GameData.Instance.PlayerGamedata[3001] >= sendingResourceCount)
        {
            GameData.Instance.PlayerGamedata[3001] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보낼 철광석 부족");
        }
    }

    public void ReceiveResources(int receivingResourceCount)
    {
        GameData.Instance.PlayerGamedata[3001] += receivingResourceCount;
    }
}
