using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudyState : MonoBehaviour, IWeatherState
{
    bool isCloudy = false;

    public bool SetWeatherState(bool weatherState)
    {
        this.isCloudy = weatherState;

        return isCloudy;
    }
}
