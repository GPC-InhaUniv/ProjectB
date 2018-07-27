using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CombinationItem
{
    Weapon = 0,
    Hat,
    Armor,
    Sheep,
}


public class ItemCombination : MonoBehaviour
{

    int requiredBrickCount;
    int requiredWoodCount;
    int requiredIronCount;
    int requiredSheepCount;
    CombinationItem currentEquipment;

    [SerializeField]
    Button[] combinationWeaponButtons;

    public void SearchEquipmentKind(string index)
    {

        int equipmentKind = Convert.ToInt32(index);

        if (equipmentKind == 0)
        {
            Debug.Log("무기 조합");
            currentEquipment = CombinationItem.Weapon;
            for(int i=0;i< combinationWeaponButtons.Length;i++)
            {
                if (GameData.Instance.equipmentCombination.WeaponCombination[i])
                {
                    combinationWeaponButtons[i].gameObject.SetActive(false);
                    Debug.Log(i + " ");
                }
            }
            Debug.Log("활성화");
        }
        else if (equipmentKind == 1)
            currentEquipment = CombinationItem.Hat;

        else if (equipmentKind == 2)
            currentEquipment = CombinationItem.Armor;

        else
            currentEquipment = CombinationItem.Sheep;



        requiredBrickCount = (equipmentKind + 1) * 10;
        requiredWoodCount = (equipmentKind + 1) * 10;
        requiredIronCount = (equipmentKind + 1) * 5;
        requiredSheepCount = (equipmentKind + 1) * 5;


    }
    

    public void CheckPlayerCanCombinate()
    {

    }



    //public bool CheckPlayerDataToInput(int brick, int wood, int iron, int sheep)
    //{

    //}


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


}


