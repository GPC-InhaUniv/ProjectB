using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDunVil : MonoBehaviour {

    string currentText;
    string village = "Vil";
    string dungeon = "Dun";

    private void Start()
    {
        currentText = village;
    }
    public void Transfom()
    {
        if(currentText == village)
        {
            currentText = dungeon;
            Debug.Log("변경됨: " + currentText);
        }
        else
        {
            currentText = village;
            Debug.Log("변경됨: " + currentText);

        }
    }
}
