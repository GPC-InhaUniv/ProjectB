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

    [SerializeField]
    Button[] combinationArmorButtons;



    public void SearchEquipmentKind(string index)
    {

        int equipmentKind = Convert.ToInt32(index);

        if (equipmentKind == 0)
        {
            Debug.Log("무기 조합");
            currentEquipment = CombinationItem.Weapon;
            for (int i = 0; i < combinationWeaponButtons.Length; i++)
            {
                Debug.Log(GameData.Instance.equipmentCombination.WeaponCombination[i]);
                if (GameData.Instance.equipmentCombination.WeaponCombination[i])
                {

                    combinationWeaponButtons[i].gameObject.SetActive(false);

                }
            }
            Debug.Log("활성화");
        }

        else if (equipmentKind == 1)
            currentEquipment = CombinationItem.Hat;

        else if (equipmentKind == 2)
        {
            currentEquipment = CombinationItem.Armor;
            Debug.Log("방어구 조합");

            for (int i = 0; i < combinationArmorButtons.Length; i++)
            {
                Debug.Log(GameData.Instance.equipmentCombination.ArmorCombination[i]);
                if (GameData.Instance.equipmentCombination.ArmorCombination[i])
                {
                    combinationArmorButtons[i].gameObject.SetActive(false);

                }
            }
            Debug.Log("활성화");


        }

        else
            currentEquipment = CombinationItem.Sheep;

    }


    public void OnClickSomeCombination(int index)
    {

        if (currentEquipment == CombinationItem.Weapon)
        {
            requiredBrickCount = index * 10;
            requiredWoodCount = index * 10;
            requiredIronCount = index * 5;
            requiredSheepCount = index * 5;
        }

        else if (currentEquipment == CombinationItem.Armor)
        {
            requiredBrickCount = index * 5;
            requiredWoodCount = index * 10;
            requiredIronCount = index * 10;
            requiredSheepCount = index * 5;
        }
        else if (currentEquipment == CombinationItem.Hat)
        {
            requiredBrickCount = index * 5;
            requiredWoodCount = index * 5;
            requiredIronCount = index * 10;
            requiredSheepCount = index * 10;
        }
        else if (currentEquipment == CombinationItem.Sheep)
        {
            requiredBrickCount = index * 10;
            requiredWoodCount = index * 5;
            requiredIronCount = index * 5;
            requiredSheepCount = index * 10;
        }

        Debug.Log("필요한 벽돌 수: " + requiredBrickCount + "필요한 나무 수:" + requiredWoodCount + "필요한 철 수:"
            + requiredIronCount + "필요한 양 수" + requiredSheepCount);
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


