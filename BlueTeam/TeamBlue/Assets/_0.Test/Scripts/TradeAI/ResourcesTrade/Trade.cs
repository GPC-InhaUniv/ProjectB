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
    int tradeProbability = 100; // 기본 확률

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
        Weather weather = new Weather();
        resourceContext = new ResourceContext();
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

        else if (tradeProbability < Random.Range(1, 100))
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
        SendResource(sendingResourcesCount, sendingResourceType, ref tradeProbability);
    }

    public void ReceiveResources(int receivingResourceCount, GameResources receivingResourceType, int tradeProbability)
    {
        int additionResource = 0;

        CheckTradeProbability(tradeProbability);
        additionResource = CheckRelationShip(relationShip);

        if (isAbleToTrade == true)
        {
            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    resourceContext.ChangeResourceState(new Brick());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    break;

                case GameResources.Iron:
                    resourceContext.ChangeResourceState(new Iron());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    break;

                case GameResources.Sheep:
                    resourceContext.ChangeResourceState(new Sheep());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    break;

                case GameResources.Wood:
                    resourceContext.ChangeResourceState(new Wood());
                    resourceContext.ReceiveResources(receivingResourceCount + additionResource);

                    break;

            }
        }

        else if(isAbleToTrade == false)
        {
            Debug.Log("거래 실패, 자원 받지 않음");

            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    //GameDataManager.Instance.PlayerGamedata[3002] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Brick"] -= receivingResourceCount;
                    break;

                case GameResources.Iron:
                    //GameDataManager.Instance.PlayerGamedata[3001] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Iron"] -= receivingResourceCount;
                    break;

                case GameResources.Sheep:
                    //GameDataManager.Instance.PlayerGamedata[3003] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Sheep"] -= receivingResourceCount;
                    break;

                case GameResources.Wood:
                    //GameDataManager.Instance.PlayerGamedata[3000] -= receivingResourceCount;
                    TestResource.Instance.testDictionary["Wood"] -= receivingResourceCount;
                    break;
            }
        }

    }

    public void SendResource(int sendingResourceCount, GameResources sendingResourceType, ref int tradeProbability)
    {
        CheckTradeProbability(tradeProbability);

        if (isAbleToTrade == true)
        {
            switch (sendingResourceType)
            {
                case GameResources.Brick:

                    resourceContext.ChangeResourceState(new Brick());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);
                    break;

                case GameResources.Iron:
            
                    resourceContext.ChangeResourceState(new Iron());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);
                    break;

                case GameResources.Sheep:
                  
                    resourceContext.ChangeResourceState(new Sheep());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);
                    break;

                case GameResources.Wood:
              
                    resourceContext.ChangeResourceState(new Wood());
                    resourceContext.CalculateTradeProbability(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    resourceContext.SendReousrces(sendingResourceCount);
                    break;
            }
        }


        else if (isAbleToTrade == false)
        {
            Debug.Log("거래 실패, 보낼 자원을 되돌려 받음");

            switch (sendingResourceType)
            {
                case GameResources.Brick:
                    //GameDataManager.Instance.PlayerGamedata[3002] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Brick"] += sendingResourceCount;
                    break;

                case GameResources.Iron:
                    //GameDataManager.Instance.PlayerGamedata[3001] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Iron"] += sendingResourceCount;
                    break;

                case GameResources.Sheep:
                    //GameDataManager.Instance.PlayerGamedata[3003] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Sheep"] += sendingResourceCount;
                    break;

                case GameResources.Wood:
                    //GameDataManager.Instance.PlayerGamedata[3000] += sendingResourcesCount;
                    TestResource.Instance.testDictionary["Wood"] += sendingResourceCount;
                    break;
            }
        }
        
    }

    public void checkwants()
    {
        CheckWantsOfResourcesAboutAI();
    }

    public GameResources CheckWantsOfResourcesAboutAI()
    {
        // 플레이어가 제일 많이 가지고 있는 자원을 원하게 바꿈

        int maxValue = 0;
        string maxValueIndex = "";

        maxValue = TestResource.Instance.testDictionary["Brick"];

        foreach(KeyValuePair<string, int> pair in TestResource.Instance.testDictionary)
        {
            if(maxValue < pair.Value)
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

        else if (maxValueIndex == "Sheep")
        { 
            checkingWantsOfResources = GameResources.Sheep;
            Debug.Log("필요한 자원 : 양");
        }

        return checkingWantsOfResources;
    }
}