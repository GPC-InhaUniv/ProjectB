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

    public void SendReousrces(int sendingResourceCount)
    {
        resource.SendResources(sendingResourceCount);
    }

    public void CalculateTradeProbability(int sendingResourceCount, GameResources resourceType, ref int tradeProbability)
    {
        resource.CalculateTradeProbability(sendingResourceCount, resourceType, ref tradeProbability);
    }
}
