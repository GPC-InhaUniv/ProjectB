using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyState : Weather
{ 
    bool isSunny = false;

    public override bool SetWeatherState(bool weatherState)
    {
        this.isSunny = weatherState;

        return isSunny;
    }
}
