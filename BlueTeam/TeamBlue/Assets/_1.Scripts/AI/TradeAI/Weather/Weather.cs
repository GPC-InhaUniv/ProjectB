using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

enum WeatherState
{
    Cloudy,
    Rainy,
    Sunny,
    Windy
}

class Weather
{
    WeatherState weatherState;
    public GameResources resourcesOfNeed;

    public void ChangeWeatherState(WeatherState weatherState)
    {
        switch (weatherState)
        {
            case WeatherState.Cloudy:
                weatherState = WeatherState.Cloudy;
                this.weatherState = weatherState;

                resourcesOfNeed = GameResources.Brick;
                //requestResourceCount = Random.Range(1, 4);
                break;

            case WeatherState.Rainy:
                weatherState = WeatherState.Rainy;
                this.weatherState = weatherState;

                resourcesOfNeed = GameResources.Wood;
                //requestResourceCount = Random.Range(1, 6);
                break;

            case WeatherState.Sunny:
                weatherState = WeatherState.Sunny;
                this.weatherState = weatherState;

                resourcesOfNeed = GameResources.Sheep;
                break;

            case WeatherState.Windy:
                weatherState = WeatherState.Windy;
                this.weatherState = weatherState;

                resourcesOfNeed = GameResources.Iron;
                //requestResourceCount = Random.Range(1, 2);
                break;
        }

    }

    public WeatherState GetWeatherState()
    {
        return weatherState;
    }

    public GameResources GetResourcesOfNeed()
    {
        Debug.Log("AI가 필요로 하는 자원 : " + resourcesOfNeed);

        return resourcesOfNeed;
    }
}