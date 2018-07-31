using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void MoveHandler(int none);

    public static event MoveHandler InputTouch;


    private void Start()
    {

    }

    public void OnPressedButton(int number)
    {
        InputTouch(number);
    }

}
