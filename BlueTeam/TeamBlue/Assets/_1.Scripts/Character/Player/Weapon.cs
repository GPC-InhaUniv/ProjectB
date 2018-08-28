using UnityEngine;

namespace ProjectB.Characters.Players
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        GameObject longSword, shortSword;
        [SerializeField]
        GameObject fakeLongSword, fakeShortSword;

        void Start()
        {
            fakeShortSword.SetActive(false);
            longSword.SetActive(false);
        }

        public void SetWeapon(bool isSet, PlayerCharacterWeaponState newState, PlayerCharacterWeaponState currentState)
        {
            if(currentState == newState)
            {
                return;
            }
            if (newState == PlayerCharacterWeaponState.LongSword)
            {
                shortSword.SetActive(!isSet);
                fakeLongSword.SetActive(!isSet);

                fakeShortSword.SetActive(isSet);
                longSword.SetActive(isSet);
            }
            else if(newState == PlayerCharacterWeaponState.ShortSword)
            {
                longSword.SetActive(!isSet);
                fakeShortSword.SetActive(!isSet);

                fakeLongSword.SetActive(isSet);
                shortSword.SetActive(isSet);
            }
        }
    }
}

