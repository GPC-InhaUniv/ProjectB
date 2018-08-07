using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    WeatherContext weatherContext;
    ResourceContext resourceContext;

    [SerializeField]
    int receivingResourcesCount;

    [SerializeField]
    int sendingResourcesCount;

    [SerializeField]
    GameResources tradeGameResources;

    [SerializeField]
    int relationShip = 100; // 기본 확률

    int specRecieveCount;

    bool isTrading = false;

    

    public void CheckTradeProbability(int relationShip)
    {
        if (relationShip >= Random.Range(1, 100))
        {
            isTrading = true;

            Debug.Log("특정 확률로 거래");
        }

        else if (relationShip < Random.Range(1, 100))
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
        //OnClick();
    }

    //public void OnClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("클릭됨");


    //    }
    //}



    public void ShowResourcesOnDebugLog()
    {
        Debug.Log("현재 자원");

        Debug.Log("흙 " + GameData.Instance.PlayerGamedata[3002]);
        Debug.Log("철광석 " + GameData.Instance.PlayerGamedata[3001]);
        Debug.Log("양 " + GameData.Instance.PlayerGamedata[3003]);
        Debug.Log("나무 " + GameData.Instance.PlayerGamedata[3000]);
        //Debug.Log("거래 확률 " + tradeProbability + "%");
        Debug.Log("우호도 " + this.relationShip);
    }

}



