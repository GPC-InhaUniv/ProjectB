using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField]
    GameObject longSword, shortSword;
    [SerializeField]
    GameObject fakeLongSword, fakeShortSword;
    // Use this for initialization
    

    void Start ()
    {

        fakeShortSword.SetActive(false);
        longSword.SetActive(false);
    }
	

    public void SetWeapon(bool isSet, PlayerCharacterWeaponState newState, PlayerCharacterWeaponState preState)
    {
        if(newState == PlayerCharacterWeaponState.LongSword && preState == PlayerCharacterWeaponState.ShortSword)
        {
            shortSword.SetActive(!isSet);
            fakeLongSword.SetActive(!isSet);

            fakeShortSword.SetActive(isSet);
            longSword.SetActive(isSet);
        }
        else
        {
            longSword.SetActive(!isSet);
            fakeShortSword.SetActive(!isSet);

            fakeLongSword.SetActive(isSet);
            shortSword.SetActive(isSet);
        }

    }


}
