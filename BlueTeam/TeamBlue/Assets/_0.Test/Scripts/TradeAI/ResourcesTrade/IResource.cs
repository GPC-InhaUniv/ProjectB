using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectB.GameManager;
interface IResource
{
    void SendResources(int sendingResourceCount);
    void ReceiveResources(int receivingResourceCount, GameResources resourceType, int tradeProbability);
}
