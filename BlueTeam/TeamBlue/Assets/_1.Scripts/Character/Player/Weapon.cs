using UnityEngine;

namespace ProjectB.Characters.Players
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        GameObject shortSword, longSword;

        [SerializeField]
        GameObject fakeShortSword, fakeLongSword;

        [SerializeField]
        Collider ShortSwordColider, LongSwordColider;

        void Start()
        {
            ShortSwordColider = shortSword.GetComponent<CapsuleCollider>();
            LongSwordColider = longSword.GetComponent<BoxCollider>();

            ShortSwordColider.enabled = false;
            LongSwordColider.enabled = false;

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

                ShortSwordColider.enabled = false;
                longSword.SetActive(isSet);
                LongSwordColider.enabled = false;
            }

            else if(newState == PlayerCharacterWeaponState.ShortSword)
            {
                longSword.SetActive(!isSet);
                fakeShortSword.SetActive(!isSet);

                fakeLongSword.SetActive(isSet);

                LongSwordColider.enabled = false;
                shortSword.SetActive(isSet);
                ShortSwordColider.enabled = false;
            }
        }
    }
}

