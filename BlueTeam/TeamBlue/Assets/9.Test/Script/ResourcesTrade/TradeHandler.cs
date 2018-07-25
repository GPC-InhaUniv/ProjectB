using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public abstract class TradeHandler
{
    protected TradeHandler nextTrade;

    public TradeHandler SetNextTrade(TradeHandler trade)
    {
        nextTrade = trade;

        return nextTrade;
    }

    public void RequestTrade(Item playerResources, Item otherResources, TownInformation relationShipInfo, int resourceCount)
    {
        if (nextTrade != null)
        {
            TradeResources(ref playerResources, ref otherResources, relationShipInfo, resourceCount);
        }

    }

    //public abstract void TradeBrick(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo);
    //public abstract void TradeWood(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo);
    //public abstract void TradeIron(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo);
    //public abstract void TradeSheep(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo);

    public abstract void TradeResources(ref Item playerResources, ref Item otherResources, TownInformation relationShipInfo, int resourceCount);
}
