using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyState : Weather
{
    bool isRainy = false;

    public override bool SetWeatherState(bool weatherState)
    {
        this.isRainy = weatherState;

        return isRainy;
    }
}
