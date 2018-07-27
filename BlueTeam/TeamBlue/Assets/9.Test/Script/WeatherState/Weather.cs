using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeatherState
{
    Sunny,
    Cloudy,
    Rainy,
    Other,
};

public abstract class Weather
{
    public abstract bool SetWeatherState(bool weatherState);
}
