using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ResourceContext : MonoBehaviour
{
    IResource resource;

    public void ChangeResourceState(IResource resource)
    {
        this.resource = resource;
    }

    public void SendResources(int sendingResourceCount)
    {
        this.resource.SendResources(sendingResourceCount);
    }

    public void ReceiveReousrces(int receivingResourceCount, GameResources resourceType, int tradeProbability)
    {
        this.resource.ReceiveResources(receivingResourceCount, resourceType, tradeProbability);
    }
}
