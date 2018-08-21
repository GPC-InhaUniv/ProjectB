using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

enum WeatherState
{
    Cloudy,
    Rainy,
    Sunny,
    Lighting 
}

class Weather
{
    WeatherState weatherState;
    GameResources resourcesOfNeed;

    int requestResourceCount = 0;

    public void ChangeWeatherState(WeatherState weatherState)
    {
        switch (weatherState)
        {
            case WeatherState.Cloudy:
                weatherState = WeatherState.Cloudy;

                resourcesOfNeed = GameResources.Brick;
                //requestResourceCount = Random.Range(1, 4);
                break;

            case WeatherState.Rainy:
                weatherState = WeatherState.Rainy;

                resourcesOfNeed = GameResources.Wood;
               //requestResourceCount = Random.Range(1, 6);
                break;

            case WeatherState.Sunny:
                weatherState = WeatherState.Sunny;

                resourcesOfNeed = GameResources.Sheep;
                break;

            case WeatherState.Lighting:
                weatherState = WeatherState.Lighting;

                resourcesOfNeed = GameResources.Iron;
                //requestResourceCount = Random.Range(1, 2);
                break;
        }

    }

    public WeatherState GetWeatherState()
    {
        if(weatherState == WeatherState.Cloudy)
        {
            Debug.Log("흐린 날씨");
        }

        else if(weatherState == WeatherState.Rainy)
        {
            Debug.Log("비오는 날씨");
        }

        else if (weatherState == WeatherState.Lighting)
        {
            Debug.Log("번개치는 날씨");
        }

        else
        {
            Debug.Log("맑은 날씨");
        }


        return weatherState;
    }

    public GameResources GetResourcesOfNeed()
    {
        Debug.Log("AI가 필요로 하는 자원 : " + resourcesOfNeed);

        return resourcesOfNeed;
    }

    //public int GetRequestResourceCount()
    //{
    //    Debug.Log("필요 자원 추가 " + requestResourceCount);

    //    return requestResourceCount;
    //}

}

