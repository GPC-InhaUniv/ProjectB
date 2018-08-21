using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

public class Trade : MonoBehaviour
{
    [SerializeField]
    int receivingResourcesCount;

    [SerializeField]
    int sendingResourcesCount;

    [SerializeField]
    int tradeProbability; // 기본 확률

    [SerializeField]
    GameResources sendingResourceType;

    [SerializeField]
    GameResources receivingResourceType;

    [SerializeField]
    int relationShip = 0;


    Weather weather;

    ResourceContext resourceContext;

    GameResources checkingWantsOfResources;

    bool isAbleToTrade = false;

    int additionResource = 0;




    void Start()
    {
        weather = new Weather();
        weather.ChangeWeatherState(WeatherState.Sunny);
    }

    public void ShowPlayerResourcesOnDebugLog()
    {
        Debug.Log("현재 자원");
        Debug.Log("");

        Debug.Log("흙 " + TestResource.Instance.testDictionary["Brick"]);
        Debug.Log("철광석 " + TestResource.Instance.testDictionary["Iron"]);
        Debug.Log("양 " + TestResource.Instance.testDictionary["Sheep"]);
        Debug.Log("나무 " + TestResource.Instance.testDictionary["Wood"]);

        Debug.Log("");
        Debug.Log("거래 확률 " + tradeProbability + "%");
        Debug.Log("우호도 " + relationShip);
    }


    public void CheckTradeProbability(int tradeProbability)
    {
        if (tradeProbability >= Random.Range(1, 100))
        {
            isAbleToTrade = true;

            Debug.Log("특정 확률로 거래");
        }

        else
        {
            isAbleToTrade = false;

            Debug.Log("특정 확률로 거래 실패");
        }
    }

    public int CheckRelationShip(int relationShip)
    {
        int specRecieveCount = 0;

        if (relationShip >= 70)
        {
            specRecieveCount = Random.Range(1, 4);

            Debug.Log("우호도가 높음, 얻는 자원" + specRecieveCount + "추가");

        }

        else if(relationShip >= 40)
        {
            Debug.Log("우호도 보통");
        }

        else if (relationShip < 40)
        {
            specRecieveCount = Random.Range(-1, -3);
           

            Debug.Log("우호도가 낮음, 얻는 자원 " + specRecieveCount + "감소");

        }

        return specRecieveCount;
    }

    public void RunReceiveResource()
    {
        ReceiveResources(receivingResourcesCount, receivingResourceType, tradeProbability);
    }

    public void RunSendResource()
    {
        SendResource(sendingResourcesCount, sendingResourceType, tradeProbability);
    }

    public void ReceiveResources(int receivingResourceCount, GameResources receivingResourceType, int tradeProbability)
    {
        additionResource = 0;

        CheckTradeProbability(tradeProbability);
        additionResource = CheckRelationShip(relationShip);

        if (isAbleToTrade == true)
        {
            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    resourceContext = new ResourceContext(new Brick());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Cloudy);
                    break;

                case GameResources.Iron:
                    resourceContext = new ResourceContext(new Iron());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Sunny);

                    break;

                case GameResources.Sheep:
                    resourceContext = new ResourceContext(new Sheep());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Lighting);

                    break;

                case GameResources.Wood:
                    resourceContext = new ResourceContext(new Wood());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Rainy);

                    break;

            }

            if (weather.GetResourcesOfNeed() == receivingResourceType)
            {
                relationShip += 1;
            }

            else
            {
                relationShip -= 3;
            }


        }

        else
        {
            Debug.Log("거래 실패, 자원 받지 않음");

            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    //GameDataManager.Instance.PlayerGamedata[3002] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Brick"] -= receivingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Sunny);
                    break;

                case GameResources.Iron:
                    //GameDataManager.Instance.PlayerGamedata[3001] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Iron"] -= receivingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Rainy);
                    break;

                case GameResources.Sheep:
                    //GameDataManager.Instance.PlayerGamedata[3003] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Sheep"] -= receivingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Lighting);
                    break;

                case GameResources.Wood:
                    //GameDataManager.Instance.PlayerGamedata[3000] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Wood"] -= receivingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Cloudy);
                    break;
            }
        }

    }

    public void SendResource(int sendingResourceCount, GameResources sendingResourceType, int tradeProbability)
    {
        CheckTradeProbability(tradeProbability);

        if (isAbleToTrade == true)
        {
            switch (sendingResourceType)
            {
                case GameResources.Brick:

                    resourceContext = new ResourceContext(new Brick());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Lighting);
                    break;

                case GameResources.Iron:
            
                    resourceContext = new ResourceContext(new Iron());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Sunny);
                    break;

                case GameResources.Sheep:
                  
                    resourceContext = new ResourceContext(new Sheep());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Rainy);
                    break;

                case GameResources.Wood:
              
                    resourceContext = new ResourceContext(new Wood());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Cloudy);
                    break;
            }

            if (weather.GetResourcesOfNeed() == receivingResourceType)
            {
                relationShip += 1;
            }

            else
            {
                relationShip -= 3;
            }

        }


        else
        {
            Debug.Log("거래 실패, 보낼 자원을 되돌려 받음");

            switch (sendingResourceType)
            {
                case GameResources.Brick:
                    //GameDataManager.Instance.PlayerGamedata[3002] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Brick"] += sendingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Lighting);
                    break;

                case GameResources.Iron:
                    //GameDataManager.Instance.PlayerGamedata[3001] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Iron"] += sendingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Cloudy);
                    break;

                case GameResources.Sheep:
                    //GameDataManager.Instance.PlayerGamedata[3003] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Sheep"] += sendingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Rainy);
                    break;

                case GameResources.Wood:
                    //GameDataManager.Instance.PlayerGamedata[3000] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Wood"] += sendingResourceCount;

                    weather.ChangeWeatherState(WeatherState.Sunny);
                    break;
            }
        }
        
    }

    public void CheckWeather()
    {
        weather.GetWeatherState();
    }

    public void CheckResourceOfNeed()
    {
        CheckResourcesOfNeedAboutAI();
    }

    public void CheckResourcesOfNeedAboutAI()
    {
        //// 플레이어가 제일 많이 가지고 있는 자원을 원함

        int maxValue = 0;
        string maxValueIndex = "";

        maxValue = TestResource.Instance.testDictionary["Brick"];

        foreach (KeyValuePair<string, int> pair in TestResource.Instance.testDictionary)
        {
            if (maxValue < pair.Value)
            {
                maxValue = pair.Value;
                maxValueIndex = pair.Key;
            }
        }


        if (maxValueIndex == "Wood")
        {
            checkingWantsOfResources = GameResources.Wood;
            Debug.Log("필요한 자원 : 나무");
        }

        else if (maxValueIndex == "Iron")
        {
            checkingWantsOfResources = GameResources.Iron;
            Debug.Log("필요한 자원 : 철광석");
        }

        else if (maxValueIndex == "Brick")
        {
            checkingWantsOfResources = GameResources.Brick;
            Debug.Log("필요한 자원 : 벽돌");
        }

        else 
        {
            checkingWantsOfResources = GameResources.Sheep;
            Debug.Log("필요한 자원 : 양");
        }
    }
}