﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using UnityEngine.UI;

//0 = Brick
//1 = Wood
//2 = Iron
//3 = Sheep

public class Trade : MonoBehaviour
{
    int tradeProbability = 50; // 거래 확률

    int relationShip = 50;

    int sendingResourceCount;

    int receivingResourceCount;


    bool isAbleToTrade = false;


    GameResources sendingResourceType;

    GameResources receivingResourceType;

    GameResources checkingWantsOfResources;


    Weather weather;

    ResourceContext resourceContext;

    public Text relationShipText;
    public Text tradeProbabilityText;

    public Text sendingResourceText;
    public Text sendingResourceCountText;

    public Text receivingResourceText;
    public Text receivingResourceCountText;

    void Start()
    {
        weather = new Weather();
        weather.ChangeWeatherState(WeatherState.Sunny);

        relationShipText = GetComponent<Text>();
        tradeProbabilityText = GetComponent<Text>();

        sendingResourceText = GetComponent<Text>();
        sendingResourceCountText = GetComponent<Text>();

        receivingResourceText = GetComponent<Text>();
        receivingResourceCountText = GetComponent<Text>();
    }

    public void Test_ShowPlayerResourcesOnDebugLog()
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


    public void CheckTradeProbability()
    {
        if (tradeProbability >= Random.Range(1, 100))
        {
            isAbleToTrade = true;

            Debug.Log("거래 확률 " + tradeProbability + "%");
            Debug.Log("특정 확률로 거래");
        }

        else
        {
            isAbleToTrade = false;

            Debug.Log("거래 확률 " + tradeProbability + "%");
            Debug.Log("특정 확률로 거래 실패");
        }

        tradeProbabilityText.text = "거래 확률 " + tradeProbability;
    }

    public void CheckRelationShip()
    {
        if (relationShip > 0)
        {
            Debug.Log("우호도 " + relationShip);

            relationShipText.text = "우호도 " + relationShip;
        }

        else 
        {
            Debug.Log("우호도 0");

            relationShipText.text = "우호도 0";
        }

    }

    public int AddReceiveCountByRelationShip()
    {
        int additionResource = 0;

        if (relationShip >= 0)
        {

            if (relationShip >= 70)
            {
                additionResource = Random.Range(1, 4);

                Debug.Log("우호도가 높음, 얻는 자원" + additionResource + "추가");

            }

            else if (relationShip >= 50 && relationShip < 70)
            {
                Debug.Log("우호도 보통");
            }

            else
            {
                additionResource = Random.Range(-1, -3);

                Debug.Log("우호도가 낮음, 얻는 자원 " + additionResource + "감소");

            }
        }

        return additionResource;

    }

    public void ReceiveResources(int receivingResourceCount)
    {
        this.receivingResourceCount = receivingResourceCount;

        CheckRelationShip();

        if (isAbleToTrade == true)
        {
            int additionResource = AddReceiveCountByRelationShip();

            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    resourceContext = new ResourceContext(new Brick());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Cloudy);

                    Debug.Log("받을 자원 : 흙, 받을 자원 개수 " + receivingResourceCount);
                    RunResourceText();
                
                    break;

                case GameResources.Iron:
                    resourceContext = new ResourceContext(new Iron());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Sunny);

                    Debug.Log("받을 자원 : 철광석, 받을 자원 개수 " + receivingResourceCount);
                    RunResourceText();

                    break;

                case GameResources.Sheep:
                    resourceContext = new ResourceContext(new Sheep());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Lighting);

                    Debug.Log("받을 자원 : 양, 받을 자원 개수 " + receivingResourceCount);
                    RunResourceText();

                    break;

                case GameResources.Wood:
                    resourceContext = new ResourceContext(new Wood());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    weather.ChangeWeatherState(WeatherState.Rainy);

                    Debug.Log("받을 자원 : 나무, 받을 자원 개수 " + receivingResourceCount);
                    RunResourceText();

                    break;

            }

            Debug.Log("추가 획득 자원 개수 " + additionResource);

            checkingWantsOfResources = weather.resourcesOfNeed;

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

            checkingWantsOfResources = weather.resourcesOfNeed;
        }

    }

    public void SetSendingResourceType(int sendingResourceTypeNumber)
    {
        sendingResourceType = (GameResources)sendingResourceTypeNumber;
    }

    public void SetReceivingResourceType(int receivingResourceTypeNumber)
    {
        receivingResourceType = (GameResources)receivingResourceTypeNumber;
    }

    public void SendResources(int sendingResourceCount)
    {
        this.sendingResourceCount = sendingResourceCount;

        CheckTradeProbability();

        if (isAbleToTrade == true)
        {
            switch (sendingResourceType)
            {
                case GameResources.Brick:

                    resourceContext = new ResourceContext(new Brick());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Lighting);

                    Debug.Log("보낼 자원 : 흙, 보낼 개수 " + sendingResourceCount);
                    RunResourceText();

                    break;

                case GameResources.Iron:

                    resourceContext = new ResourceContext(new Iron());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Sunny);

                    Debug.Log("보낼 자원 : 철광석, 보낼 개수 " + sendingResourceCount);
                    RunResourceText();

                    break;

                case GameResources.Sheep:

                    resourceContext = new ResourceContext(new Sheep());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Rainy);

                    Debug.Log("보낼 자원 : 양, 보낼 개수 " + sendingResourceCount);
                    RunResourceText();

                    break;

                case GameResources.Wood:

                    resourceContext = new ResourceContext(new Wood());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    weather.ChangeWeatherState(WeatherState.Cloudy);

                    Debug.Log("보낼 자원 : 나무, 보낼 개수 " + sendingResourceCount);
                    RunResourceText();

                    break;
            }

            checkingWantsOfResources = weather.resourcesOfNeed;
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

            checkingWantsOfResources = weather.resourcesOfNeed;
        }
    }

    public void CheckWeather()
    {
        weather.GetWeatherState();
    }

    public void CheckResourcesOfNeedAboutAI()
    {
        // 플레이어가 가장 많이 가지고 있는 자원을 원함

        int maxValue = 0;
        string maxValueKey = "";

        maxValue = TestResource.Instance.testDictionary["Brick"];

        foreach (KeyValuePair<string, int> pair in TestResource.Instance.testDictionary)
        {
            if (maxValue < pair.Value)
            {
                maxValue = pair.Value;
                maxValueKey = pair.Key;
            }
        }


        if (maxValueKey == "Wood")
        {
            checkingWantsOfResources = GameResources.Wood;
            Debug.Log("필요한 자원 : 나무");
        }

        else if (maxValueKey == "Iron")
        {
            checkingWantsOfResources = GameResources.Iron;
            Debug.Log("필요한 자원 : 철광석");
        }

        else if (maxValueKey == "Brick")
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

    public void RunResourceText()
    {
        switch(sendingResourceType)
        {
            case GameResources.Brick:
                sendingResourceText.text = "보낼 자원 : 흙";
                sendingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;

            case GameResources.Iron:
                sendingResourceText.text = "보낼 자원 : 철광석";
                sendingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;

            case GameResources.Sheep:
                sendingResourceText.text = "보낼 자원 : 양";
                sendingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;

            case GameResources.Wood:
                sendingResourceText.text = "보낼 자원 : 나무";
                sendingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;


        }

        switch (receivingResourceType)
        {
            case GameResources.Brick:
                receivingResourceText.text = "받을 자원 : 흙";
                receivingResourceCountText.text = "받을 개수 : " + sendingResourceCount;
                break;

            case GameResources.Iron:
                receivingResourceText.text = "받을 자원 : 철광석";
                receivingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;

            case GameResources.Sheep:
                receivingResourceText.text = "받을 자원 : 양";
                receivingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;

            case GameResources.Wood:
                receivingResourceText.text = "받을 자원 : 나무";
                receivingResourceCountText.text = "보낼 개수 : " + sendingResourceCount;
                break;


        }
    }
}