﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour {
    public Transform attachPoint;
    public Transform Weapon;

	// Use this for initialization
	void Start () {
        Weapon.parent = attachPoint;
        Weapon.position = attachPoint.position;
        Weapon.rotation = attachPoint.rotation;
	}
}
