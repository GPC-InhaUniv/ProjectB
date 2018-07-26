using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 개수에 따라서 받는 자원 개수가 달라짐

public class TradingResources : TradeHandler
{
    public override void TradeResources(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo, int resourceCount)
    { 



        //Item tempResources = new Item();
        //tempResources = playerResources;

        //playerResources = otherResources;
        //otherResources= tempResources;

        if (relationShipInfo.RelationsShip < 10)
        {
            // 받는 자원 양 감소
        }

        if (relationShipInfo.RelationsShip >= 50)
        {
            // 받는 자원 추가
        }
        
    }
}
