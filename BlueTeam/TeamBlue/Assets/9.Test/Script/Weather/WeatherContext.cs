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

class ResourceContext : MonoBehaviour
{
    IResource resources;

    public void SetWeatherContext(IResource resourceType)
    {
        this.resources = resourceType;
    }

    public void RequestSettingWeather()
    {

    }

}

