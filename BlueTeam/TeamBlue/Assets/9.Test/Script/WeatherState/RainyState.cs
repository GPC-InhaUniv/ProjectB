using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyState : MonoBehaviour, IWeatherState
{
    bool isRainy = false;

    public bool SetWeatherState(bool weatherState)
    {
        this.isRainy = weatherState;

        return isRainy;
    }
}
