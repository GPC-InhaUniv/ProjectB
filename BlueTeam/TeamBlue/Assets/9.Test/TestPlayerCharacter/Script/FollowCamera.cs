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
    Transform _target = null;


	void Start ()
    {
        camRotation = new Vector3(45, -45, 0);
        camPosition = new Vector3(4, 5, -2);
        SetTarget(player);
    }
	void SetTarget(GameObject target)
    {
        //_target = target.transform;
    }
    private void LateUpdate()
    {
        if (_target == null) return;

        transform.position = _target.position + camPosition;
        transform.localRotation = Quaternion.Euler(camRotation);
    }
}
