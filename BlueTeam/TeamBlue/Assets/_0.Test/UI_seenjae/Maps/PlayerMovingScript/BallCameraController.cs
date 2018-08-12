using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    private float rotationSpeed;

	void Start () {
        offset = transform.position - player.transform.position;
        rotationSpeed = 5;
	}

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
    }

    void Update () {
	}
}
