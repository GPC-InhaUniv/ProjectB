using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EquipItemSlot : MonoBehaviour {

    Inventory inventory;

	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipItem()
    {

    }
}
