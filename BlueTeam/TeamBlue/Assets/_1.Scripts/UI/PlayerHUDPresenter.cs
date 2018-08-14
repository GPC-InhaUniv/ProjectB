using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters;

namespace ProjectB.UI
{
    public class PlayerHUDPresenter : MonoBehaviour
    {
        Character player;
        

        public Image HPBar;
        public Text LevelText;
        private void Update()
        {
           
        }

        public void ShowHUD()
        {
            HPBar.fillAmount = (float)player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            
        }


    }
}
