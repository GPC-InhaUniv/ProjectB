using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameResources
{
    Brick,
    Wood,
    Iron,
    Sheep,

}

public class GameData : Singleton<GameData>
{
    
    private int brick;
    private int wood;
    private int iron;
    private int sheep;


    public void ChangeResource(GameResources type, int count)
    {
        switch (type)
        {
            case GameResources.Brick:
                brick += count;
                break;
            case GameResources.Wood:
                wood += count;
                break;
            case GameResources.Iron:
                iron += count;
                break;
            case GameResources.Sheep:
                sheep += count;
                break;
            default:
                break;
        }
    }

}
