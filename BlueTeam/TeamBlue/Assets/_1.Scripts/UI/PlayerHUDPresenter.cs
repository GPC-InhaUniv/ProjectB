using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters;

namespace ProjectB.UI
{
    public class PlayerHUDPresenter : MonoBehaviour
    {
        [SerializeField]
        Character player;
        
        [SerializeField]
       private Image hPBar;
        [SerializeField]
        private Text levelText;
       

        public void ShowHUD()
        {
            hPBar.fillAmount = (float)player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            levelText.text = "Level\n"+player.CharacterLevel.ToString();
            
        }


    }
}
