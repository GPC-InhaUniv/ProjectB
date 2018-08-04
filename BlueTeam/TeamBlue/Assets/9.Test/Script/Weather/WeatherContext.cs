using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeatherState
{
    Cloudy,
    Rainy,
    Sunny,
    Lighting
}

class WeatherContext : MonoBehaviour
{
    IWeather weatherState;

    public void SetWeatherContext(IWeather weatherState)
    {
        this.weatherState = weatherState;
    }

    public void RequestSettingWeather()
    {
        this.weatherState.SetWeather();
    }

}

