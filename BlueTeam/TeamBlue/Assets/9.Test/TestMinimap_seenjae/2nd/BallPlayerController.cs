using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayerController : MonoBehaviour {

    //private Rigidbody rb;
    //private float speed;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    speed = 10.0f;
    //}
    //private void FixedUpdate()
    //{
    //    float moveHorizontal = Input.GetAxis("Horizontal");
    //    float moveVertical = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
    //    rb.AddForce(movement*speed);

    //}

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * 5.0f * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 5.0f * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 5.0f * Time.deltaTime);

        }
    }
}
