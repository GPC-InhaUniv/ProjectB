using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

class ResourceContext
{
    IResource resource;

    public ResourceContext(IResource resource)
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

    public int CalculateTradeProbability(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        return resource.CalculateTradeProbability(receivingResourceCount, resourceType, tradeProbability);
    }
}
