using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour
{
    WeatherContext weatherContext;

    //[SerializeField]
    //int resourceRecieveCount;
    //int resourceSendingCount;

    //int relationship;

    int probability = 100;

    public void TradeResources(Item playerResources, int resourceRecieveCount, int resourceSendingCount, int relationship, GameResources gameResources)
    {

        switch (gameResources)
        {
            case GameResources.Brick:
                playerResources.Brick -= resourceSendingCount;
                playerResources.Brick += resourceRecieveCount;
                probability -= resourceRecieveCount;

                break;

            case GameResources.Iron:
                playerResources.Iron -= resourceSendingCount;
                playerResources.Iron += resourceRecieveCount;
                probability -= resourceRecieveCount;

                break;

            case GameResources.Sheep:
                playerResources.Sheep -= resourceSendingCount;
                playerResources.Sheep += resourceRecieveCount;
                probability -= resourceRecieveCount;

                break;

            case GameResources.Wood:
                playerResources.Wood -= resourceSendingCount;
                playerResources.Wood += resourceRecieveCount;
                probability -= resourceRecieveCount;

                break;

            case GameResources.SpecialItem:
                playerResources.SpecialItem -= resourceSendingCount;
                playerResources.SpecialItem += resourceRecieveCount;
                probability -= resourceRecieveCount;

                break;
        }

        if (relationship < 10)
        {
            resourceRecieveCount -= Random.Range(1, 2);
        }

        if (relationship >= 50)
        {
            resourceRecieveCount += Random.Range(1, 4);
        }



        //Item tempResources = new Item();
        //tempResources = playerResources;

        //playerResources = otherResources;
        //otherResources= tempResources;
    }
}
