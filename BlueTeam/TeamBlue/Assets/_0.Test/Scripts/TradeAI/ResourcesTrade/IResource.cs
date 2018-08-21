using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectB.GameManager;

// 나무 : 3000, 양 : 3003, 철광석 : 3001, 벽돌 : 3002

interface IResource
{
    void ReceiveResources(int receivingResourceCount);
    void SendResources(int sendingResourceCount);
    int CalculateTradeProbability(int sendingResourceCount, GameResources resourceType, int tradeProbability);
}
