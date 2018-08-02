using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour {
    [SerializeField]
    GameObject LongSword, ShortSword;
    [SerializeField]
    GameObject FakeLongSword, FakeShortSword;
    // Use this for initialization
    

    void Start ()
    {

        FakeShortSword.SetActive(false);
        LongSword.SetActive(false);
    }
	

    public void SetWeapon(bool isSet, PlayerCharacterWeaponState newState, PlayerCharacterWeaponState preState)
    {
        if(newState == PlayerCharacterWeaponState.LongSword && preState == PlayerCharacterWeaponState.ShortSword)
        {
            ShortSword.SetActive(!isSet);
            FakeLongSword.SetActive(!isSet);

            FakeShortSword.SetActive(isSet);
            LongSword.SetActive(isSet);
        }
        else
        {
            LongSword.SetActive(!isSet);
            FakeShortSword.SetActive(!isSet);

            FakeLongSword.SetActive(isSet);
            ShortSword.SetActive(isSet);
        }

    }


}
