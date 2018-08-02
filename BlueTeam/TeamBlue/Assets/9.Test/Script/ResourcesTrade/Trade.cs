using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    WeatherContext resourceContext;

    [SerializeField]
    int receivingResourcesCount;

    [SerializeField]
    int sendingResourcesCount;

    [SerializeField]
    int relationShip;

    [SerializeField]
    GameResources tradeGameResources;

    [SerializeField]
    int tradeProbability = 100; // 기본 확률

    int specRecieveCount;

    bool isTrading = false;

    int Brick=0;
    int Sheep=0;
    int Wood=0;
    int Iron=0;

    public void TradeResources(int receivingResourcesCount, int sendingResourcesCount, int relationShip, GameResources gameResources)
    {
        
        if (Brick >= sendingResourcesCount && Iron >= sendingResourcesCount &&
            Sheep >= sendingResourcesCount && Wood >= sendingResourcesCount)
        {
            CheckTradeProbability(tradeProbability);
            CheckRelationShip(relationShip);

            if (isTrading == true)
            {

                switch (gameResources)
                {
                    case GameResources.Brick:

                        Debug.Log("벽돌 교환");

                        Brick -= sendingResourcesCount;
                        Brick += receivingResourcesCount; 
                        tradeProbability -= (receivingResourcesCount - specRecieveCount);


                        ShowResourcesOnDebugLog();

                        break;



                    case GameResources.Iron:

                        Debug.Log("철 교환");

                        Iron -= sendingResourcesCount;
                        Iron += receivingResourcesCount;
                        tradeProbability -= (receivingResourcesCount - specRecieveCount);


                        ShowResourcesOnDebugLog();

                        break;


                    case GameResources.Sheep:

                        Debug.Log("양 교환");

                        Sheep -= sendingResourcesCount;
                        Sheep += receivingResourcesCount;
                        tradeProbability -= (receivingResourcesCount - specRecieveCount);


                        ShowResourcesOnDebugLog();

                        break;


                    case GameResources.Wood:

                        Debug.Log("나무 교환");

                        Wood -= sendingResourcesCount;
                        Wood += receivingResourcesCount; 
                        tradeProbability -= (receivingResourcesCount - specRecieveCount);


                        ShowResourcesOnDebugLog();

                        break;
                }
            }
        }

        else
        {
            Debug.Log("교환할 자원 부족");
        }
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

            ShowResourcesOnDebugLog();
        }
    }

    public void CheckRelationShip(int relationShip)
    {
        if (relationShip >= 50)
        {
            specRecieveCount = Random.Range(1, 4);

            Debug.Log("우호도가 높음, 1 ~ 4 사이의 자원 개수 중 " + specRecieveCount + "추가");

        }

        else
        {
            specRecieveCount = Random.Range(1, 3);

            Debug.Log("우호도가 낮음, 1 ~ 3 사이의 받을 자원 개수 중 " + specRecieveCount + "감소");

        }
    }



    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("클릭됨");

            TradeResources(this.receivingResourcesCount, this.sendingResourcesCount, this.relationShip, tradeGameResources);


        }
    }



    public void ShowResourcesOnDebugLog()
    {
        Debug.Log("현재 자원");

        Debug.Log("벽돌 " + Brick);
        Debug.Log("철 " + Iron);
        Debug.Log("양 " + this.Sheep);
        Debug.Log("나무 " + this.Wood);
        Debug.Log("거래 확률 " + tradeProbability + "%");
        Debug.Log("우호도 " + this.relationShip);
    }

}



