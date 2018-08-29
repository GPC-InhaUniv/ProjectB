using System.Collections;
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

    int sendingResourceCount = 0;

    int receivingResourceCount = 0;


    bool isAbleToTrade = false;

    GameResources sendingResourceType;

    GameResources receivingResourceType;

    GameResources checkingWantsOfResources;


    Weather weather;

    ResourceContext resourceContext;


    [SerializeField]
    Text relationShipText;
    [SerializeField]
    Text tradeProbabilityText;
    [SerializeField]
   Text sendingResourceText;
    [SerializeField]
    Text sendingResourceCountText;

    [SerializeField]
    Text receivingResourceText;
    [SerializeField]
    Text receivingResourceCountText;
    
    [SerializeField]
    Text resourceCountText;

    [SerializeField]
    Text weatherText;
    [SerializeField]
    GameObject selectButton;

    private void OnEnable()
    {
        selectButton.SetActive(true);
    }

    void Start()
    {
        weather = new Weather();
        weather.ChangeWeatherState(WeatherState.Sunny);

        resourceCountText.text = "ResourceCount : " + sendingResourceCount;
        tradeProbabilityText.text = "Success Rate  : " + tradeProbability;
        relationShipText.text = "RelationShip : " + relationShip;

        RunWeatherText();

    }

    public void IncreaseSendingResourceCount()
    {
        sendingResourceCount += 1;

        resourceCountText.text = "ResourceCount : " + sendingResourceCount;
    }

    public void DecreaseSendingResourceCount()
    {
        sendingResourceCount -= 1;

        if (sendingResourceCount < 0)
        {
            sendingResourceCount = 0;
        }

        resourceCountText.text = "ResourceCount : " + sendingResourceCount;
    }

    public void RunWeatherText()
    {
        if (weather.GetWeatherState() == WeatherState.Sunny)
        {
            weatherText.text = "Weather : Sunny";
        }

        else if(weather.GetWeatherState() == WeatherState.Rainy)
        {
            weatherText.text = "Weather : Rainy";
        }

        else if (weather.GetWeatherState() == WeatherState.Cloudy)
        {
            weatherText.text = "Weather : Cloudy";
        }

        else
        {
            weatherText.text = "Weather : Windy";
        }


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

        tradeProbabilityText.text = "Success Rate " + tradeProbability;
    }

    public void CheckRelationShip()
    {
        if (relationShip > 0)
        {
            Debug.Log("우호도 " + relationShip);

            relationShipText.text = "RelationShip " + relationShip;
        }

        else 
        {
            Debug.Log("우호도 0");

            relationShipText.text = "RelationShip 0";
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

    public void ReceiveResources()
    {
        CheckRelationShip();
       
        if (isAbleToTrade == true)
        {
            int additionResource = AddReceiveCountByRelationShip();
            receivingResourceCount = sendingResourceCount + additionResource;
            receivingResourceType = (GameResources)Random.Range(0, 3);
            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    resourceContext = new ResourceContext(new Brick());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    Debug.Log("받을 자원 : 흙, 받을 자원 개수 " + receivingResourceCount);

                    break;

                case GameResources.Iron:
                    resourceContext = new ResourceContext(new Iron());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    Debug.Log("받을 자원 : 철광석, 받을 자원 개수 " + receivingResourceCount);

                    break;

                case GameResources.Sheep:
                    resourceContext = new ResourceContext(new Sheep());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    Debug.Log("받을 자원 : 양, 받을 자원 개수 " + receivingResourceCount);

                    break;

                case GameResources.Wood:
                    resourceContext = new ResourceContext(new Wood());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    Debug.Log("받을 자원 : 나무, 받을 자원 개수 " + receivingResourceCount);

                    break;
            }

            weather.ChangeWeatherState((WeatherState)Random.Range(0, 3));

            Debug.Log("추가 획득 자원 개수 " + additionResource);

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
            Debug.Log("거래 실패");
        }

        weather.ChangeWeatherState((WeatherState)Random.Range(0, 3));

        checkingWantsOfResources = weather.resourcesOfNeed;
        RunWeatherText();

    }

    public void SetSendingResourceType(int sendingResourceTypeNumber)
    {
        sendingResourceType = (GameResources)sendingResourceTypeNumber;
    }

    public void SetReceivingResourceType(int receivingResourceTypeNumber)
    {
        receivingResourceType = (GameResources)receivingResourceTypeNumber;
    }

    public void SendResources()
    {
        CheckTradeProbability();
        selectButton.SetActive(false);
        if (isAbleToTrade == true)
        {
            switch (sendingResourceType)
            {
                case GameResources.Brick:

                    resourceContext = new ResourceContext(new Brick());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    Debug.Log("보낼 자원 : 흙, 보낼 개수 " + sendingResourceCount);

                    break;

                case GameResources.Iron:

                    resourceContext = new ResourceContext(new Iron());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    Debug.Log("보낼 자원 : 철광석, 보낼 개수 " + sendingResourceCount);

                    break;

                case GameResources.Sheep:

                    resourceContext = new ResourceContext(new Sheep());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    Debug.Log("보낼 자원 : 양, 보낼 개수 " + sendingResourceCount);

                    break;

                case GameResources.Wood:

                    resourceContext = new ResourceContext(new Wood());

                    tradeProbability = resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);

                    Debug.Log("보낼 자원 : 나무, 보낼 개수 " + sendingResourceCount);

                    break;
            }
          
        }

        else
        {
            Debug.Log("거래 실패");
        }
       
        ReceiveResources();

        weather.ChangeWeatherState((WeatherState)Random.Range(0, 3));

        checkingWantsOfResources = weather.resourcesOfNeed;
        RunWeatherText();
        RunResourceText();
        
    }

    public void CheckResourcesOfNeedAboutAI()
    {
        // 플레이어가 가장 많이 가지고 있는 자원을 원함

        int maxValue = 0;
        
        int maxValueKey = 0;
 

        for(int i = 3000;i < 3004;i++) 
        {
            if (GameDataManager.Instance.PlayerGamedata[i] > maxValue)
            {
                maxValue = GameDataManager.Instance.PlayerGamedata[i];
                maxValueKey = i;
            }
            
        }


        if (maxValueKey == 3000)
        {
            checkingWantsOfResources = GameResources.Wood;
            Debug.Log("필요한 자원 : 나무");
        }

        else if (maxValueKey == 3001)
        {
            checkingWantsOfResources = GameResources.Iron;
            Debug.Log("필요한 자원 : 철광석");
        }

        else if (maxValueKey == 3002)
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
                receivingResourceCountText.text = "받을 개수 : " + receivingResourceCount;
                break;

            case GameResources.Iron:
                receivingResourceText.text = "받을 자원 : 철광석";
                receivingResourceCountText.text = "받을 개수 : " + receivingResourceCount;
                break;

            case GameResources.Sheep:
                receivingResourceText.text = "받을 자원 : 양";
                receivingResourceCountText.text = "받을 개수 : " + receivingResourceCount;
                break;

            case GameResources.Wood:
                receivingResourceText.text = "받을 자원 : 나무";
                receivingResourceCountText.text = "받을 개수 : " + receivingResourceCount;
                break;


        }
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

  


}