using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 나무 : 3000, 양 : 3003, 철광석 : 3001, 벽돌 : 3002

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
