using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherContext : MonoBehaviour
{
    Weather weatherState;

    public void SetWeatherContext(Weather weather)
    {
        this.weatherState = weatherState;
    }

    public void SetWeatherRequest(bool isWeatherOn)
    {
        weatherState.SetWeatherState(isWeatherOn);
    }

}

