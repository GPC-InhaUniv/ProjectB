using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WeatherState
{
    Cloudy,
    Rainy,
    Sunny,
    Lighting // 날씨 하나 추가(정확히 무슨 날씨인지 모름)
}

class Weather : MonoBehaviour
{
    WeatherState weatherState;

    public void ChangeWeatherState(WeatherState weatherState)
    {
        switch (weatherState)
        {
            case WeatherState.Cloudy:
                weatherState = WeatherState.Cloudy;
                break;

            case WeatherState.Rainy:
                weatherState = WeatherState.Rainy;
                break;

            case WeatherState.Sunny:
                weatherState = WeatherState.Sunny;
                break;

            case WeatherState.Lighting:
                weatherState = WeatherState.Lighting;
                break;
        }

    }

    public WeatherState GetWeatherState()
    {
        return weatherState;
    }

}

