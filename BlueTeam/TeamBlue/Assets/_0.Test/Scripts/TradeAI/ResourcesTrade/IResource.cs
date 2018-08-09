using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IResource
{
    void SendResources(int sendingResourceCount);
    void ReceiveResources(int receivingResourceCount, GameResources resourceType, ref int tradeProbability);
}
