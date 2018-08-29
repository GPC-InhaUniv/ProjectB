using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

interface ResourceContext
{
    void ReceiveResources(int receivingResourceCount);
    void SendReousrces(int sendingResourceCount);
    int CalculateTradeProbability(int receivingResourceCount, GameResources resourceType, int tradeProbability);
}
