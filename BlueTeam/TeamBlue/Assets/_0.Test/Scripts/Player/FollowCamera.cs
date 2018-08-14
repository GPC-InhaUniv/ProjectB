using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 camRotation;
    [SerializeField]
    Vector3 camPosition;
    Transform target = null;


	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        target = player.transform;

        camRotation = new Vector3(45, -45, 0);
        camPosition = new Vector3(4, 5, -2);
        SetTarget(player);
    }
	void SetTarget(GameObject GoalTarget)
    {
        target = GoalTarget.transform;
    }
    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + camPosition;
        transform.localRotation = Quaternion.Euler(camRotation);
    }
}
