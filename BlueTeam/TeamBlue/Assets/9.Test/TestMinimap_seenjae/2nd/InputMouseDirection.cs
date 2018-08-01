using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseDirection : MonoBehaviour {

    private float sensibilityX;

    private void Start()
    {
        sensibilityX = 1.5f;
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensibilityX, 0);
    }
}
