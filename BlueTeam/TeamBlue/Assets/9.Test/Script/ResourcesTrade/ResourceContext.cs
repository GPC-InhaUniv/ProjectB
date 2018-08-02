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

    public void RunTradeResource(GameResources gameResources)
    {
        this.resource.TradeResources();
    }
}
