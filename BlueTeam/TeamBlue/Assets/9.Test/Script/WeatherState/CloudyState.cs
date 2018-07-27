using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudyState : Weather
{
    bool isCloudy = false;

    public override bool SetWeatherState(bool weatherState)
    {
        this.isCloudy = weatherState;

        return isCloudy;
    }
}
