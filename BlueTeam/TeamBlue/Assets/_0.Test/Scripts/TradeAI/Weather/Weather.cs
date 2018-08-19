using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

enum WeatherState
{
    Cloudy,
    Rainy,
    Sunny,
    Lighting // 날씨 하나 추가
}

class Weather : MonoBehaviour
{
    WeatherState weatherState;

    int requestResource = 0;

    public void ChangeWeatherState(WeatherState weatherState)
    {
        switch (weatherState)
        {
            case WeatherState.Cloudy:
                weatherState = WeatherState.Cloudy;
                requestResource = Random.Range(1, 4);
                Debug.Log("필요한 자원 추가 " + requestResource);
                break;

            case WeatherState.Rainy:
                weatherState = WeatherState.Rainy;
                requestResource = Random.Range(1, 6);
                Debug.Log("필요한 자원 추가" + requestResource);
                break;

            case WeatherState.Sunny:
                weatherState = WeatherState.Sunny;
                Debug.Log("필요한 자원 추가 " + requestResource);
                break;

            case WeatherState.Lighting:
                weatherState = WeatherState.Lighting;
                requestResource = Random.Range(1, 2);
                Debug.Log("필요한 자원 추가 " + requestResource);
                break;
        }

    }

    public WeatherState GetWeatherState()
    {
        return weatherState;
    }

    public int GetRequestResource()
    {
        return requestResource;
    }

}

