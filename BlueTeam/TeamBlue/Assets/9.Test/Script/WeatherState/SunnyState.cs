using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyState : MonoBehaviour, IWeatherState
{
    bool isSunny = false;

    public bool SetWeatherState(bool weatherState)
    {
        this.isSunny = weatherState;

        return isSunny;
    }
}
