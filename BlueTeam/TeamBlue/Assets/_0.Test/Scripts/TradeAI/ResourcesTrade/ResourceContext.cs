using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class ResourceContext : MonoBehaviour
{
    IResource resource;

    public void ChangeResourceState(IResource resource)
    {
        this.resource = resource;
    }

    public void ReceiveResources(int receivingResourceCount)
    {
        resource.ReceiveResources(receivingResourceCount);
    }

    public void SendReousrces(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        resource.SendResources(sendingResourceCount, resourceType, ref tradeProbability);
    }

    public bool CheckTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        return resource.CheckTradeProbability(sendingResourceCount, resourceType, ref tradeProbability);
    }
}
