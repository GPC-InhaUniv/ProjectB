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

    int specRecieveCount = 0;

    bool isAbleToTrade = false;




    void Start()
    {
        Weather weather = new Weather();
        resourceContext = new ResourceContext();
    }

    void Update()
    {

    }

    public void ShowPlayerResourcesOnDebugLog()
    {
        Debug.Log("현재 자원");
        Debug.Log("");

        Debug.Log("흙 " + GameDataManager.Instance.PlayerGamedata[3002]);
        Debug.Log("철광석 " + GameDataManager.Instance.PlayerGamedata[3001]);
        Debug.Log("양 " + GameDataManager.Instance.PlayerGamedata[3003]);
        Debug.Log("나무 " + GameDataManager.Instance.PlayerGamedata[3000]);

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

    public void CheckRelationShip(int relationShip)
    {
        int tempReceiveCount = 0;

        if (relationShip >= 50)
        {
            tempReceiveCount = Random.Range(1, 4);
            specRecieveCount = tempReceiveCount;
            tempReceiveCount = 0;

            Debug.Log("우호도가 높음, 1 ~ 4 사이의 자원 개수 중 " + specRecieveCount + "추가");

        }

        else if (relationShip < 50)
        {
            tempReceiveCount = Random.Range(1, 3);
            specRecieveCount = tempReceiveCount;
            tempReceiveCount = 0;

            Debug.Log("우호도가 낮음, 1 ~ 3 사이의 받을 자원 개수 중 " + specRecieveCount + "감소");

        }
    }

    public void ReceiveResources(int receivingResourceCount, GameResources receivingResourceType)
    {
        if (isAbleToTrade == true)
        {
            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    resourceContext.ChangeResourceState(new Brick());
                    resourceContext.ReceiveResources(receivingResourceCount);

                    break;

                case GameResources.Iron:
                    resourceContext.ChangeResourceState(new Iron());
                    resourceContext.ReceiveResources(receivingResourceCount);

                    break;

                case GameResources.Sheep:
                    resourceContext.ChangeResourceState(new Sheep());
                    resourceContext.ReceiveResources(receivingResourceCount);

                    break;

                case GameResources.Wood:
                    resourceContext.ChangeResourceState(new Wood());
                    resourceContext.ReceiveResources(receivingResourceCount);

                    break;

            }
        }

        else if(isAbleToTrade == false)
        {
            Debug.Log("거래 실패, 자원 받지 않음");

            switch (receivingResourceType)
            {
                case GameResources.Brick:
                    GameDataManager.Instance.PlayerGamedata[3002] -= receivingResourceCount;
                    break;

                case GameResources.Iron:
                    GameDataManager.Instance.PlayerGamedata[3001] -= receivingResourceCount;
                    break;

                case GameResources.Sheep:
                    GameDataManager.Instance.PlayerGamedata[3003] -= receivingResourceCount;
                    break;

                case GameResources.Wood:
                    GameDataManager.Instance.PlayerGamedata[3000] -= receivingResourceCount;
                    break;
            }
        }

    }

    public void SendResource(int sendingResourceCount, GameResources sendingResourceType, ref int tradeProbability)
    {
        if (isAbleToTrade == true)
        {
            switch (sendingResourceType)
            {
                case GameResources.Brick:

                    resourceContext.ChangeResourceState(new Brick());
                    resourceContext.SendReousrces(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    break;

                case GameResources.Iron:
            
                    resourceContext.ChangeResourceState(new Iron());
                    resourceContext.SendReousrces(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    break;

                case GameResources.Sheep:
                  
                    resourceContext.ChangeResourceState(new Sheep());
                    resourceContext.SendReousrces(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    break;

                case GameResources.Wood:
              
                    resourceContext.ChangeResourceState(new Wood());
                    resourceContext.SendReousrces(sendingResourceCount, sendingResourceType, ref tradeProbability);
                    break;
            }
        }


        else if (isAbleToTrade == false)
        {
            Debug.Log("거래 실패, 보낼 자원을 되돌려 받음");

            switch (sendingResourceType)
            {
                case GameResources.Brick:
                    GameDataManager.Instance.PlayerGamedata[3002] += sendingResourcesCount;
                    break;

                case GameResources.Iron:
                    GameDataManager.Instance.PlayerGamedata[3001] += sendingResourcesCount;
                    break;

                case GameResources.Sheep:
                    GameDataManager.Instance.PlayerGamedata[3003] += sendingResourcesCount;
                    break;

                case GameResources.Wood:
                    GameDataManager.Instance.PlayerGamedata[3000] += sendingResourcesCount;
                    break;
            }
        }
        
    }

    public void checkwants()
    {
        CheckWantsOfResourcesOnAI();
    }

    public GameResources CheckWantsOfResourcesOnAI()
    {
        // 플레이어가 제일 많이 가지고 있는 자원을 원하게 변경

        int maxValue = 0;
        int maxValueIndex = 0;

        maxValue = GameDataManager.Instance.PlayerGamedata[3000];

        for (int i = 3000; i <= 3003; i++)
        {
            if (maxValue > GameDataManager.Instance.PlayerGamedata[i])
            {
                maxValue = GameDataManager.Instance.PlayerGamedata[i];

                maxValueIndex = i;
            }

        }

        if (maxValueIndex == 3000)
        {
            checkingWantsOfResources = GameResources.Wood;
            Debug.Log("필요한 자원 : 나무");
        }

        else if (maxValueIndex == 3001)
        {
            checkingWantsOfResources = GameResources.Iron;
            Debug.Log("필요한 자원 : 철광석");
        }

        else if (maxValueIndex == 3002)
        {
            checkingWantsOfResources = GameResources.Brick;
            Debug.Log("필요한 자원 : 벽돌");
        }

        else if (maxValueIndex == 3003)
        { 
            checkingWantsOfResources = GameResources.Sheep;
            Debug.Log("필요한 자원 : 양");
        }

        return checkingWantsOfResources;
    }
}