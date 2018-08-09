﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

public class Trade : MonoBehaviour
{
    Weather weather;
    ResourceContext resourceContext;

    [SerializeField]
    int receivingResourcesCount;

    [SerializeField]
    int sendingResourcesCount;

    [SerializeField]
    int tradeProbability = 100; // 기본 확률

    //[SerializeField]
    //GameResources resourceType;

    [SerializeField]
    int relationShip = 0;

    int specRecieveCount = 0;

    bool isTrading = false;



    void Start()
    {
        Weather weather = new Weather();
        resourceContext = new ResourceContext();
    }


    void Update()
    {
        OnClicked();
    }


    public void CheckTradeProbability(int tradeProbability)
    {
        if (tradeProbability >= Random.Range(1, 100))
        {
            isTrading = true;

            Debug.Log("특정 확률로 거래");
        }

        else if (tradeProbability < Random.Range(1, 100))
        {
            isTrading = false;

            Debug.Log("특정 확률로 거래 실패");

            //ShowResourcesOnDebugLog();
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

        else if(relationShip < 50)
        {
            tempReceiveCount = Random.Range(1, 3);
            specRecieveCount = tempReceiveCount;
            tempReceiveCount = 0;

            Debug.Log("우호도가 낮음, 1 ~ 3 사이의 받을 자원 개수 중 " + specRecieveCount + "감소");

        }
    }

    public void TradeResources(int receivingResourcesCount, int sendingResourcesCount, ref int tradeProbability, 
        GameResources sendingResourceType, GameResources receivingResourceType)
    {
        int minValue = 0;

        if (isTrading)
        {
            if(sendingResourceType == GameResources.Brick)
            {
                resourceContext.ChangeResourceState(new Brick());
                resourceContext.SendResources(sendingResourcesCount);

                // 플레이어가 가진 자원 중 가장 적은 자원을 상대가 원하는 자원으로

                
            }

            if (sendingResourceType == GameResources.Iron)
            {
                resourceContext.ChangeResourceState(new Iron());
                resourceContext.SendResources(sendingResourcesCount);



            }

            if (sendingResourceType == GameResources.Sheep)
            {
                resourceContext.ChangeResourceState(new Sheep());
                resourceContext.SendResources(sendingResourcesCount);


            }

            if (sendingResourceType == GameResources.Wood)
            {
                resourceContext.ChangeResourceState(new Wood());
                resourceContext.SendResources(sendingResourcesCount);


            }

        }

        else
        {
            Debug.Log("거래 취소");
        }
    }

    public void OnClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("클릭됨");

            CheckRelationShip(relationShip);

            //CheckTradeProbability(tradeProbability);

            //TradeResources(receivingResourcesCount, sendingResourcesCount, ref tradeProbability, resourceType);


        }
    }

    //public void ShowResourcesOnDebugLog()
    //{
    //    Debug.Log("현재 자원");

    //    Debug.Log("흙 " + GameDataManager.Instance.PlayerGamedata[3002]);
    //    Debug.Log("철광석 " + GameDataManager.Instance.PlayerGamedata[3001]);
    //    Debug.Log("양 " + GameDataManager.Instance.PlayerGamedata[3003]);
    //    Debug.Log("나무 " + GameDataManager.Instance.PlayerGamedata[3000]);

    //    Debug.Log("거래 확률 " + tradeProbability + "%");
    //    Debug.Log("우호도 " + relationShip);
    //}








}



