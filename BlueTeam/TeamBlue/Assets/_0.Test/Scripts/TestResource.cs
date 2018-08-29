using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResource : Singleton<TestResource>
{

    public int BrickCount;
    public int IronCount;
    public int SheepCount;
    public int WoodCount;
    public Dictionary<string, int> testDictionary;

    private static TestResource testResource;

    private TestResource()
    {
    }

    private void Start()
    {
        testDictionary = new Dictionary<string, int>
        {
            { "Brick", BrickCount },
            { "Iron", IronCount },
            { "Sheep", SheepCount },
            { "Wood", WoodCount }
        };
    }

    //    public static TestResource GetInstance()
    //    {
    //        if(testResource == null)
    //        {
    //            testResource = new TestResource();
    //        }

    //        return testResource;
    //    }
}
