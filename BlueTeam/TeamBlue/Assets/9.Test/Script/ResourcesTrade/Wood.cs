using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wood : MonoBehaviour, IResource
{
    public void SendResources(int sendingResourceCount)
    {
        if (GameData.Instance.PlayerGamedata[3000] >= sendingResourceCount)
  
        {
            GameData.Instance.PlayerGamedata[3000] -= sendingResourceCount;
        }

        else
        {
            Debug.Log("보내는 나무 부족");
        }


    }

    public void ReceiveResources(int receivingResourceCount)
    {
        GameData.Instance.PlayerGamedata[3000] += receivingResourceCount;
    }
}
